using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;
using NovaTechIMS.Models;
using NovaTechIMS.Models.Enums;

namespace NovaTechIMS.Data;

/// <summary>ADO.NET access for Delegation (Milestone 15).</summary>
public class DelegationRepository
{
    public IReadOnlyList<DelegationListRow> GetList(
        int? toUserId = null,
        string? status = null,
        string? responsibility = null)
    {
        var list = new List<DelegationListRow>();
        var where = new List<string>();

        if (toUserId is not null)
            where.Add("d.\"DelegatedToUserID\" = @toUser");
        if (!string.IsNullOrWhiteSpace(status))
            where.Add("d.\"Status\" = @status");
        if (!string.IsNullOrWhiteSpace(responsibility))
            where.Add("d.\"Responsibility\" = @resp");

        var whereSql = where.Count == 0 ? string.Empty : "WHERE " + string.Join(" AND ", where);

        var sql = $"""
            SELECT d."DelegationID",
                   d."DelegatedByUserID",
                   byu."FullName" AS "DelegatedByName",
                   d."DelegatedToUserID",
                   tou."FullName" AS "DelegatedToName",
                   tou."Username" AS "DelegatedToUsername",
                   d."Responsibility",
                   d."StartDate",
                   d."EndDate",
                   d."Reason",
                   d."Status",
                   d."CreatedDateTime",
                   d."RevokedByUserID",
                   d."RevokedDateTime"
            FROM "Delegation" d
            INNER JOIN "User" byu ON byu."UserID" = d."DelegatedByUserID"
            INNER JOIN "User" tou ON tou."UserID" = d."DelegatedToUserID"
            {whereSql}
            ORDER BY d."CreatedDateTime" DESC
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);

        if (toUserId is not null)
            cmd.Parameters.Add(new NpgsqlParameter("toUser", NpgsqlDbType.Integer) { Value = toUserId.Value });
        if (!string.IsNullOrWhiteSpace(status))
            cmd.Parameters.Add(new NpgsqlParameter("status", NpgsqlDbType.Varchar) { Value = status });
        if (!string.IsNullOrWhiteSpace(responsibility))
            cmd.Parameters.Add(new NpgsqlParameter("resp", NpgsqlDbType.Varchar) { Value = responsibility });

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            list.Add(MapListRow(reader));

        return list;
    }

    /// <summary>
    /// Active rows for user where Status=Active and StartDate &lt;= today &lt;= EndDate.
    /// </summary>
    public IReadOnlyList<DelegatableResponsibility> GetValidResponsibilities(int userId, DateTime asOfDate)
    {
        var list = new List<DelegatableResponsibility>();

        const string sql = """
            SELECT "Responsibility"
            FROM "Delegation"
            WHERE "DelegatedToUserID" = @userId
              AND "Status" = 'Active'
              AND "StartDate" <= @asOf
              AND "EndDate" >= @asOf
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("userId", NpgsqlDbType.Integer) { Value = userId });
        cmd.Parameters.Add(new NpgsqlParameter("asOf", NpgsqlDbType.Date) { Value = asOfDate.Date });

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var r = ParseResponsibility(reader.GetString(0));
            if (!list.Contains(r))
                list.Add(r);
        }

        return list;
    }

    public bool HasOverlappingActive(
        int toUserId,
        DelegatableResponsibility responsibility,
        DateTime start,
        DateTime end,
        int? excludeDelegationId = null)
    {
        const string sql = """
            SELECT COUNT(*)::int
            FROM "Delegation"
            WHERE "DelegatedToUserID" = @toUser
              AND "Responsibility" = @resp
              AND "Status" = 'Active'
              AND "StartDate" <= @endDate
              AND "EndDate" >= @startDate
              AND (@excludeId IS NULL OR "DelegationID" <> @excludeId)
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("toUser", NpgsqlDbType.Integer) { Value = toUserId });
        cmd.Parameters.Add(new NpgsqlParameter("resp", NpgsqlDbType.Varchar) { Value = ResponsibilityToDb(responsibility) });
        cmd.Parameters.Add(new NpgsqlParameter("startDate", NpgsqlDbType.Date) { Value = start.Date });
        cmd.Parameters.Add(new NpgsqlParameter("endDate", NpgsqlDbType.Date) { Value = end.Date });
        cmd.Parameters.Add(new NpgsqlParameter("excludeId", NpgsqlDbType.Integer)
        {
            Value = excludeDelegationId.HasValue ? excludeDelegationId.Value : DBNull.Value
        });

        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }

    public int Insert(Delegation d)
    {
        const string sql = """
            INSERT INTO "Delegation"
                ("DelegatedByUserID", "DelegatedToUserID", "Responsibility",
                 "StartDate", "EndDate", "Reason", "Status", "CreatedDateTime")
            VALUES
                (@byUser, @toUser, @resp, @start, @end, @reason, @status, @created)
            RETURNING "DelegationID"
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("byUser", NpgsqlDbType.Integer) { Value = d.DelegatedByUserID });
        cmd.Parameters.Add(new NpgsqlParameter("toUser", NpgsqlDbType.Integer) { Value = d.DelegatedToUserID });
        cmd.Parameters.Add(new NpgsqlParameter("resp", NpgsqlDbType.Varchar) { Value = ResponsibilityToDb(d.Responsibility) });
        cmd.Parameters.Add(new NpgsqlParameter("start", NpgsqlDbType.Date) { Value = d.StartDate.Date });
        cmd.Parameters.Add(new NpgsqlParameter("end", NpgsqlDbType.Date) { Value = d.EndDate.Date });
        cmd.Parameters.Add(new NpgsqlParameter("reason", NpgsqlDbType.Varchar) { Value = d.Reason.Trim() });
        cmd.Parameters.Add(new NpgsqlParameter("status", NpgsqlDbType.Varchar) { Value = "Active" });
        cmd.Parameters.Add(new NpgsqlParameter("created", NpgsqlDbType.TimestampTz) { Value = d.CreatedDateTime });

        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void Revoke(int delegationId, int revokedByUserId, DateTime revokedAt)
    {
        const string sql = """
            UPDATE "Delegation"
            SET "Status" = 'Revoked',
                "RevokedByUserID" = @byUser,
                "RevokedDateTime" = @revokedAt
            WHERE "DelegationID" = @id
              AND "Status" = 'Active'
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = delegationId });
        cmd.Parameters.Add(new NpgsqlParameter("byUser", NpgsqlDbType.Integer) { Value = revokedByUserId });
        cmd.Parameters.Add(new NpgsqlParameter("revokedAt", NpgsqlDbType.TimestampTz) { Value = revokedAt });

        if (cmd.ExecuteNonQuery() == 0)
            throw new InvalidOperationException("Delegation was not active or was not found.");
    }

    public Delegation? GetById(int id)
    {
        const string sql = """
            SELECT "DelegationID", "DelegatedByUserID", "DelegatedToUserID", "Responsibility",
                   "StartDate", "EndDate", "Reason", "Status", "CreatedDateTime",
                   "RevokedByUserID", "RevokedDateTime"
            FROM "Delegation"
            WHERE "DelegationID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = id });

        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        return new Delegation
        {
            DelegationID = reader.GetInt32(reader.GetOrdinal("DelegationID")),
            DelegatedByUserID = reader.GetInt32(reader.GetOrdinal("DelegatedByUserID")),
            DelegatedToUserID = reader.GetInt32(reader.GetOrdinal("DelegatedToUserID")),
            Responsibility = ParseResponsibility(reader.GetString(reader.GetOrdinal("Responsibility"))),
            StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
            EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
            Reason = reader.GetString(reader.GetOrdinal("Reason")),
            Status = ParseStatus(reader.GetString(reader.GetOrdinal("Status"))),
            CreatedDateTime = reader.GetDateTime(reader.GetOrdinal("CreatedDateTime")),
            RevokedByUserID = reader.IsDBNull(reader.GetOrdinal("RevokedByUserID"))
                ? null
                : reader.GetInt32(reader.GetOrdinal("RevokedByUserID")),
            RevokedDateTime = reader.IsDBNull(reader.GetOrdinal("RevokedDateTime"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("RevokedDateTime"))
        };
    }

    /// <summary>Mark Active rows past EndDate as Expired (lazy maintenance).</summary>
    public void ExpirePastDue(DateTime asOfDate)
    {
        const string sql = """
            UPDATE "Delegation"
            SET "Status" = 'Expired'
            WHERE "Status" = 'Active'
              AND "EndDate" < @asOf
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("asOf", NpgsqlDbType.Date) { Value = asOfDate.Date });
        cmd.ExecuteNonQuery();
    }

    private static DelegationListRow MapListRow(NpgsqlDataReader reader)
    {
        var resp = reader.GetString(reader.GetOrdinal("Responsibility"));
        var status = reader.GetString(reader.GetOrdinal("Status"));
        return new DelegationListRow
        {
            DelegationID = reader.GetInt32(reader.GetOrdinal("DelegationID")),
            DelegatedByUserID = reader.GetInt32(reader.GetOrdinal("DelegatedByUserID")),
            DelegatedByName = reader.GetString(reader.GetOrdinal("DelegatedByName")),
            DelegatedToUserID = reader.GetInt32(reader.GetOrdinal("DelegatedToUserID")),
            DelegatedToName = reader.GetString(reader.GetOrdinal("DelegatedToName")),
            DelegatedToUsername = reader.GetString(reader.GetOrdinal("DelegatedToUsername")),
            Responsibility = ParseResponsibility(resp),
            ResponsibilityLabel = ResponsibilityLabel(resp),
            StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
            EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
            Reason = reader.GetString(reader.GetOrdinal("Reason")),
            Status = ParseStatus(status),
            StatusLabel = status,
            CreatedDateTime = reader.GetDateTime(reader.GetOrdinal("CreatedDateTime")),
            RevokedDateTime = reader.IsDBNull(reader.GetOrdinal("RevokedDateTime"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("RevokedDateTime"))
        };
    }

    private static DelegatableResponsibility ParseResponsibility(string value)
        => value switch
        {
            "StockIn" => DelegatableResponsibility.StockIn,
            "StockOut" => DelegatableResponsibility.StockOut,
            "ReportAccess" => DelegatableResponsibility.ReportAccess,
            _ => DelegatableResponsibility.StockIn
        };

    private static string ResponsibilityToDb(DelegatableResponsibility r)
        => r switch
        {
            DelegatableResponsibility.StockIn => "StockIn",
            DelegatableResponsibility.StockOut => "StockOut",
            DelegatableResponsibility.ReportAccess => "ReportAccess",
            _ => "StockIn"
        };

    private static string ResponsibilityLabel(string db)
        => db switch
        {
            "StockIn" => "Stock-In",
            "StockOut" => "Stock-Out",
            "ReportAccess" => "Report Access",
            _ => db
        };

    private static DelegationStatus ParseStatus(string s)
        => s switch
        {
            "Active" => DelegationStatus.Active,
            "Expired" => DelegationStatus.Expired,
            "Revoked" => DelegationStatus.Revoked,
            _ => DelegationStatus.Active
        };
}

public class DelegationListRow
{
    public int DelegationID { get; set; }
    public int DelegatedByUserID { get; set; }
    public string DelegatedByName { get; set; } = string.Empty;
    public int DelegatedToUserID { get; set; }
    public string DelegatedToName { get; set; } = string.Empty;
    public string DelegatedToUsername { get; set; } = string.Empty;
    public DelegatableResponsibility Responsibility { get; set; }
    public string ResponsibilityLabel { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DelegationStatus Status { get; set; }
    public string StatusLabel { get; set; } = string.Empty;
    public DateTime CreatedDateTime { get; set; }
    public DateTime? RevokedDateTime { get; set; }
}

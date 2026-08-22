# Change Record — Database Engine Amendment

**Date:** August 2026  
**Branch:** `version1.2`  
**Status:** APPROVED  

---

## Summary

| | |
|--|--|
| **OLD** | SQL Server LocalDB |
| **NEW** | PostgreSQL |
| **Scope** | Implementation technology only — no SRS / Product / UX / UI change |
| **Milestone impact** | Milestone 3 scripts rewritten; Milestone 4 will use Npgsql (not implemented yet) |

## Reason

Existing developer environment already provides PostgreSQL. Avoiding unnecessary installation of a second database server while preserving all required relational functionality (PK/FK, CHECK, UNIQUE, transactions, partial unique index for active delegations, soft-delete policy, append-only inventory history by application rule).

## Suitability checklist

| Requirement | PostgreSQL support |
|-------------|-------------------|
| Seven tables | Yes |
| PK / FK | Yes |
| UNIQUE | Yes |
| CHECK | Yes |
| Transactions | Yes |
| Non-negative qty/prices | CHECK constraints |
| Inventory history | Table + app append-only rule |
| Delegation rules | Table + partial unique index |
| Soft-delete / NO ACTION FKs | Yes |
| Timestamps | TIMESTAMPTZ |
| Generated IDs | SERIAL |

## Final status

**APPROVED**

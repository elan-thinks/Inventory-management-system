# 14 — Testing Strategy

**Document:** Testing  
**Version:** 1.0  

---

## 1. Testing Philosophy

Appropriate for a university project:

- Focus on business rules and critical paths.
- Prefer automated unit tests for services where practical.
- Manual / exploratory testing for UI flows.
- Explicit negative-case list that must be demonstrated.

---

## 2. Test Types

| Type | Scope | Tooling |
|------|-------|---------|
| Unit | Service methods with mocked or in-memory data access | MSTest / NUnit / xUnit |
| Integration | Real LocalDB, full service + repository | Same + test database |
| UI / Manual | Forms, navigation, validation messages | Checklist |
| Authorization | Permission matrix | Unit + manual |
| Delegation | Valid / expired / revoked / overlapping | Unit + manual |

---

## 3. Mandatory Negative Cases (must be tested)

1. Stock-Out with quantity > QuantityOnHand → rejected, quantity unchanged.
2. Stock quantity reaching exactly zero → status becomes Out of Stock.
3. Low-stock boundary (QuantityOnHand == MinimumStockLevel) → Low Stock status.
4. Inventory Adjustment to a new quantity (including to zero) → history row + correct QuantityOnHand.
5. Unauthorized Staff attempts Adjustment / User Management / Delegation → AuthorizationException.
6. Valid delegation grants the extra permission only inside the date range.
7. Expired delegation grants nothing.
8. Revoked delegation grants nothing.
9. Attempt to create overlapping delegation for same recipient + responsibility → rejected.
10. Attempt to delegate a non-delegatable responsibility → rejected.
11. Duplicate Category name / Username → rejected.
12. Delete Product that has transactions → blocked; Deactivate offered.
13. Stock-In / Stock-Out on inactive product → rejected.
14. Product creation leaves QuantityOnHand = 0 and creates no transaction.

---

## 4. Example Unit Test Sketches

```csharp
[TestMethod]
public void RecordStockOut_InsufficientQuantity_ThrowsAndDoesNotChangeStock()
{
    // arrange product with Qty = 5
    // act & assert InsufficientStockException
    // assert QuantityOnHand still 5
}

[TestMethod]
public void HasPermission_WithValidDelegation_ReturnsTrue()
{
    // arrange Staff user + active StockIn delegation for today
    // assert true for RecordStockIn
}

[TestMethod]
public void HasPermission_WithExpiredDelegation_ReturnsFalse()
{
    // arrange delegation that ended yesterday
    // assert false
}
```

---

## 5. Acceptance Criteria for Testing Milestone

- All 14 negative cases above pass.
- Happy-path CRUD for every master entity works.
- Stock-In / Stock-Out / Adjustment produce correct history and quantity.
- Login / logout / role visibility work.
- SCR-020 create / revoke / history display work.

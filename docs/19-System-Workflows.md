# 19. System Workflows

## WF-001 — User Login

1. Application starts → Login form appears
2. User enters username + password
3. User clicks Login
4. System checks credentials
5. On success → load role → open Dashboard
6. On failure → show error → stay on Login

## WF-002 — Add New Product

1. User navigates to Products → Add New
2. System opens Product form (Category & Supplier ComboBoxes populated)
3. User completes fields
4. User clicks Save
5. Client-side validation runs
6. If valid → save to database → success message → refresh list / close form
7. If invalid → show messages → user corrects and retries

## WF-003 — Edit Product

1. User selects product in grid → clicks Edit
2. Form opens with current values
3. User changes data → Save
4. Validation → update → refresh

## WF-004 — Delete Product

1. User selects product → Delete
2. Confirmation dialog appears
3. User confirms
4. System checks for related transactions
5. If clear → delete (or soft-delete) → refresh
6. If blocked → informative message

## WF-005 — Receive Stock (Stock-In)

1. User opens Stock-In form
2. Selects Product (and optionally Supplier)
3. Enters Quantity, Date, Price, Notes
4. Clicks Save
5. Validation (quantity > 0, product active, etc.)
6. Create transaction record
7. Update Product.QuantityOnHand += Quantity
8. Success feedback + optional print receipt

## WF-006 — Issue Stock (Stock-Out)

1. User opens Stock-Out form
2. Selects Product and Customer
3. Enters Quantity, Date, Selling Price
4. System (or user) can see current available stock
5. Save → validation including stock availability
6. If sufficient → create transaction → decrease QuantityOnHand
7. If insufficient → reject with available quantity shown

## WF-007 — Search Inventory

1. User is on Product List
2. Types in search box and/or selects filters
3. Grid updates to show matching products
4. User can clear filters to see all

## WF-008 — Identify Low-Stock Items

1. User opens Low Stock view (or applies filter / uses Dashboard link)
2. System shows only products where 0 < Qty ≤ Min Level
3. User can drill into a product or generate Low Stock Report

## WF-009 — View Transaction History

1. User opens Transaction History form
2. Optionally sets date range, type, product
3. Clicks Search / Apply
4. Grid displays matching transactions
5. User may export or print

## WF-010 — Generate Inventory Report

1. User opens Reports menu
2. Selects report type
3. Sets any filters (dates, category, etc.)
4. Clicks Generate
5. Report appears in a viewer form
6. User may Print

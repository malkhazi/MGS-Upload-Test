Public Class Cl_OutletDebtsDetail
    Public Property id As String = ""
    Public Property checkId As Long = 0
    Public Property invoice_no As String ' ზედნადების #

    ' ბანკი 1 ვადაგადაუცილებელი, 2 ვადაგადაცილებული,
    ' ნაღდი 3 ვადაგადაუცილებელი, 4 ვადაგადაცილებული
    Public Property debtypcode As String
    Public Property merch_id As Integer = 0
    Public Property merch_code As String ' პრისეილერი
    Public Property debt As Decimal ' ზედნ თანხა ვალი
    Public Property [date] As String
    Public Property comment As String = ""
    Public Property qty As Decimal ' საქონლის ჯამი
    Public Property d_overdue As Decimal = 0 ' ვადაგადაცილებული ვალი
    Public Property d_ov_delay As Integer = 0 ' ვადაგადაცილებული დღეები
    Public Property document As String = ""
    Public Property status As Int16 = 2
    Public Property dtlm As String
    Public Property cust_id As Integer

End Class

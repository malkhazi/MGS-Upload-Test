Public Class Cl_OutletDebt
    Public Property id As String = ""
    Public Property debt As Decimal 'ჯამური ვალი
    Public Property ol_code As Cl_SubID ' ობიექტის კოდი
    Public Property paydate As String 'ბოლო გადახდა
    Public Property date_calc As String 'ბოლო გადახდა
    Public Property cansale As Boolean ' ნებადართვა რეალიზაციაზე
    Public Property avg_amount As Decimal = 0 ' საშუალო თვიური ბრუნვა

    Public Property status As Int16 = 2
    Public Property dtlm As String
    Public Property cust_id As Integer
    Public Property outletDebtsDetail As List(Of Cl_OutletDebtsDetail)

    ' არა აუცილებელი ველები
    'Public Property details As String
    'Public Property maxdebt As Decimal
    'Public Property maxdelay As Integer
    'Public Property d_overdue As Decimal
    'Public Property cur_delay As Integer
    'Public Property d_ov_delay As Integer
    'Public Property pcomp_code As String
End Class

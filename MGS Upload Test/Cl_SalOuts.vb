Public Class Cl_SalOuts
    Public Property id As String = ""
    Public Property invoice_no As String ' ზედნადების ID
    Public Property order_no As String ' შეკვეთის #
    Public Property [date] As String '
    Public Property dateto As DateTime ' იგივე თარიღი
    Public Property wareh_code As String = "" ' საწყობის კოდი
    Public Property doc_type As Int16 '2 გაყიდვა 4 დაბრუნება
    Public Property vatcalcmod As Integer = 1 ' დღგ

    Public Property status As Int16 = 2 '
    Public Property salOutLocalDetail As List(Of Cl_salOutLocalDetail)
    Public Property dtlm As String '
    Public Property cust_id As Integer '
    Public Property merch_id As Int32 = 0 ' პრისეილერი
    Public Property ol_code As Cl_SubID  ' ობიექტის კოდი

    Public Property merch_code As String = ""
    Public Property param1 As Int32 = 1
    Public Property printorder As Boolean = False
    Public Property printcheck As Boolean = False
    Public Property prnchkonly As Boolean = False
    Public Property cinvoic_no As String = ""

    'Public Property loc_code As String = ""
    'Public Property pcomp_code As String = ""

End Class

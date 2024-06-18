Public Class Cl_SalIn
    Public Property id As String = ""
    Public Property invoice_no As String
    Public Property [date] As String
    Public Property wareh_code As String = ""
    Public Property doc_type As Int16
    Public Property custdoc_no As String = ""
    Public Property vatcalcmod As Integer = 1
    Public Property status As Int16 = 2
    Public Property dtlm As String
    Public Property cust_id As Integer
    Public Property salInLocalDetail As List(Of Cl_SalInLocalDetail)

End Class

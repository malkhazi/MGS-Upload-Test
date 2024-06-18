Public Class Cl_Outlets
    'Public Property version As Integer = 1 '????? gaurkveveli veli

    Public Property id As String = "0" 'X
    Public Property ol_id As Long = 0 '+
    Public Property subtype_id As Integer = 0 '+X
    Public Property area_id As Integer = 0 '+X
    Public Property owner_id As Integer = 0 'X
    Public Property status As Integer = 2 '+X
    Public Property cust_id As Integer = 0 '+X
    Public Property lic_usage As Integer = 1 'X


    'Public Property id As String = "0"  '
    'Public Property ol_id As String = "0" '+
    'Public Property subtype_id As String = "0"  '+
    'Public Property area_id As String = "0"  '+
    'Public Property owner_id As String = "0"  '
    'Public Property status As String = "2"  '+
    'Public Property cust_id As String = "0"  '+
    'Public Property lic_usage As String = "0" '

    Public Property merch_code As String = "-" '+X
    Public Property pcomp_code As Cl_Pcomp_Code  '------ ar sachiroebs chashlas
    Public Property ol_code As String = "-" '+X
    Public Property name As String = "-" '+X
    Public Property trade_name As String = "-" '+X
    Public Property director As String = "-" 'X
    Public Property address As String = "-" '+X
    Public Property deliv_addr As String = "-" '+X
    Public Property telephone As String = "-" '+X
    Public Property fax As String = "-" 'X
    Public Property email As String = "-" 'X
    Public Property accountant As String = "-" 'X
    Public Property acc_phone As String = "-" 'X
    Public Property m_manager As String = "-" 'X
    Public Property mm_phone As String = "-" 'X
    Public Property p_manager As String = "-" 'X
    Public Property open_time As String = "00:00" 'X
    Public Property close_time As String = "00:00" 'X
    Public Property break_from As String = "00:00" 'X
    Public Property break_to As String = "00:00" 'X
    Public Property zkpo As String = "-" 'X
    Public Property ipn As String = "-" '+X
    Public Property vatn As String = "-" 'X
    Public Property rr As String = "-" 'X
    Public Property bankcode As String = "-" 'X
    Public Property bankname As String = "-" 'X
    Public Property bankaddr As String = "-" 'X
    Public Property dtlm As String = "-" '+X

    'არააუცილებელი
    Public Property contr_num As String = "-" 'X
    Public Property contr_date As String = "2020-01-01" 'X
    Public Property dc_allow As Integer = 0 'X
    Public Property oldistcent As String = "-" 'X
    Public Property oldistshar As Decimal = 1.0 'X
    Public Property dc_deliver As Boolean = False 'X
    Public Property dc_payer As Boolean = False 'X დღგ-ს გადამხდელი
    Public Property cntr_dt_f As String = "" 'X
    'Dim a11 As String = "[" & vbCrLf & "    {" & vbCrLf & "        ""id"": 3," & vbCrLf & "        ""accountant"": ""a""," & vbCrLf &
    '                "        ""owner_id"": 1," & vbCrLf & "        ""ol_code"": ""ss""," & vbCrLf & "        ""open_time"": ""a""," &
    '                vbCrLf & "        ""area_id"": 1," & vbCrLf & "        ""contr_date"": ""2024-01-18""," & vbCrLf &
    '                "        ""p_manager"": ""a""," & vbCrLf & "        ""merch_code"": ""s""," & vbCrLf &
    '                "        ""bankaddr"": ""a""," & vbCrLf & "        ""fax"": ""a""," & vbCrLf &
    '                "        ""subtype_id"": 1," & vbCrLf & "        ""bankcode"": ""a""," & vbCrLf &
    '                "        ""m_manager"": ""a""," & vbCrLf & "        ""director"": ""a""," & vbCrLf &
    '                "        ""ipn"": ""12345678910""," & vbCrLf & "        ""break_to"": ""hh:mm""," & vbCrLf &
    '                "        ""telephone"": ""a""," & vbCrLf & "        ""close_time"": ""hh:mm""," & vbCrLf &
    '                "        ""contr_num"": ""a""," & vbCrLf & "        ""dc_allow"": 1," & vbCrLf &
    '                "        ""version"": 1," & vbCrLf & "        ""lic_usage"": 1," & vbCrLf &
    '                "        ""cntr_dt_f"": ""2024-01-18""," & vbCrLf & "        ""acc_phone"": ""a""," & vbCrLf &
    '                "        ""name"": ""a""," & vbCrLf & "        ""deliv_addr"": ""a""," & vbCrLf &
    '                "        ""oldistshar"": 1.000," & vbCrLf & "        ""cust_id"": 1," & vbCrLf &
    '                "        ""dtlm"": ""2024-01-26T12:00:00""," & vbCrLf & "        ""status"": 0," & vbCrLf &
    '                "        ""rr"": ""a""," & vbCrLf & "        ""break_from"": ""00:00""," & vbCrLf &
    '                "        ""oldistcent"": ""a""," & vbCrLf & "        ""dc_deliver"": true," & vbCrLf &
    '                "        ""zkpo"": ""a""," & vbCrLf & "        ""trade_name"": ""a""," & vbCrLf &
    '                "        ""mm_phone"": ""a""," & vbCrLf & "        ""vatn"": ""a""," & vbCrLf &
    '                "        ""email"": ""a""," & vbCrLf & "        ""address"": ""a""," & vbCrLf &
    '                "        ""dc_payer"": true," & vbCrLf & "        ""pcomp_code"": {" & vbCrLf &
    '                "            ""id"": ""12345678910""" & vbCrLf & "        }," & vbCrLf & "        ""ol_id"": 3," & vbCrLf &
    '                "        ""bankname"": ""a""" & vbCrLf & "    }" & vbCrLf & "]"
End Class

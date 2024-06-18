
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Module Module1
    Public wrapper As New Simple3Des("srtera@5543")
    Public M_time_Str11 As String

    Public M_SrvIP As String

    Public M_flg_atv1 As Boolean = True

    Public My_httpClient As New HttpClient()
    Public _MyTokenData As String = ""

    Public M_momc_SN As String = ""

    Public M_Cur_ClientId As String = ""
    Public M_Cur_ClientSecret As String = ""
    Public M_Cur_ClientNik As String = ""
    Public M_Cur_cust_id As Integer = 0

    Public M_ClientNik1 As String = ""
    Public M_ClientId1 As String = ""
    Public M_ClientSecret1 As String = ""
    Public M_cust_id1 As Integer = 0

    Public M_ClientNik2 As String = ""
    Public M_ClientId2 As String = ""
    Public M_ClientSecret2 As String = ""
    Public M_cust_id2 As Integer = 0

    Public SQL_conn_string As String = ""
    Public SqlClient_Tmp_con_string As String = ""

    Private Token_time1 As DateTime = New DateTime(2000, 1, 1)

    Public cDT_goods As New DataTable
    Public cDT_OutletDebtDet As New DataTable

    Public Async Function JS_Get_TokenAsync(cr_HttpClien As HttpClient, Optional Pflg_Cam_New As Boolean = False) As Task(Of Rt_Class)
        Dim rt_01 As New Rt_Class
        If Not Pflg_Cam_New Then
            If Not (_MyTokenData.Length = 0 Or DateDiff(DateInterval.Minute, token_time1, DateTime.Now) > 50) Then
                rt_01.Rt_flg = True
                Return rt_01
            End If
        End If
        Dim tokenIP1 As String = M_SrvIP & "/oauth2/token"
        Dim request As New HttpRequestMessage(HttpMethod.Post, tokenIP1)
        Dim rt_txt1 As String
        Try
            If M_Cur_ClientId.Trim.Length = 0 Then
                M_Cur_ClientId = M_ClientId1
                M_Cur_ClientSecret = M_ClientSecret1
                M_Cur_ClientNik = M_ClientNik1
                M_Cur_cust_id = M_cust_id1
            End If

            Dim byteArray As Byte() = Encoding.ASCII.GetBytes($"{M_Cur_ClientId}:{M_Cur_ClientSecret}")
            cr_HttpClien.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray))

            Dim content As New FormUrlEncodedContent(New Dictionary(Of String, String) From {
                {"grant_type", "client_credentials"}
            })
            request.Content = content
            Dim response As HttpResponseMessage = Await cr_HttpClien.SendAsync(request)

            ' პასუხის შემოწმება (სტატუსის კოდი 200 OK)
            If response.IsSuccessStatusCode Then
                ' პასუხის როგორც ტექსტის წაკითხვა
                Dim responseBody As String = Await response.Content.ReadAsStringAsync()

                ' ტოკენის მიღება.
                rt_txt1 = responseBody
                Dim jsonData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody)
                _MyTokenData = jsonData("access_token")
                token_time1 = DateTime.Now
                rt_01.Rt_flg = True
                rt_01.Rt_txt = _MyTokenData
            Else
                ' წარუმატებელი პასუხის გაშიფრვა
                rt_01.Rt_txt = $"შეცდომა: {response.StatusCode} - {response.ReasonPhrase}"
                rt_01.Rt_flg = False
            End If
        Catch ex As Exception
            ' შეცდომის კოდი
            rt_01.Rt_txt = $"შეცდომა: {ex.Message}"
            rt_01.Rt_flg = False
        End Try
        Return rt_01
    End Function

    Public Async Function JS_DownloadData(Pr_FuncName As String, Optional par2 As String = "") As Task(Of Rt_Class)
        Dim rt_vl1 As New Rt_Class
        rt_vl1.Rt_txt = ""
        Try
            Dim rt_token As Rt_Class = Await JS_Get_TokenAsync(My_httpClient)
            If Not rt_token.Rt_flg Then
                rt_vl1.Rt_txt = "წვდომის ტოკენის მიღება ვერ მოხერხდა!"
                Return rt_vl1
            End If
            Dim requestDataUrl As String = String.Format(M_SrvIP & "/rest/entities/{0}?dynamicAttributes=true{1}",
                                                         Pr_FuncName, par2)
            My_httpClient.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _MyTokenData)
            Dim response As HttpResponseMessage = Await My_httpClient.GetAsync(requestDataUrl)
            If response.IsSuccessStatusCode Then
                Dim rt_txt2 As String = Await response.Content.ReadAsStringAsync()
                Dim jsonToken2 As JToken = JsonConvert.DeserializeObject(Of JArray)(rt_txt2)
                If jsonToken2.Type = JTokenType.Array Then
                    rt_vl1.Rt_Dt = JsonConvert.DeserializeObject(Of DataTable)(jsonToken2.ToString())
                    rt_vl1.Rt_flg = True
                Else
                    rt_vl1.Rt_txt = "ტექსტი არ შეიცავს JTokenType.Array ტიპის მონაცემებს"
                End If
            Else
                rt_vl1.Rt_txt = "API-დან მონაცემების მოძიება ვერ მოხერხდა. სტატუსის კოდი: " & response.StatusCode.ToString()
            End If
        Catch ex As Exception
            rt_vl1.Rt_txt = "დაშვებულია შეცდომა: " & ex.Message
        End Try
        Return rt_vl1
    End Function

    Public Async Function JS_DownloadCurentID(Pr_FuncName As String, Pr_ID As String, Pr_Dm_par1 As String) As Task(Of Rt_Class)
        Dim rt_vl55 As New Rt_Class
        rt_vl55.Rt_Flg = False
        Try
            Dim rt_token As Rt_Class = Await JS_Get_TokenAsync(My_httpClient)
            If Not rt_token.Rt_flg Then
                rt_vl55.Rt_txt = "წვდომის ტოკენის მიღება ვერ მოხერხდა!"
                Return rt_vl55
            End If

            Dim requestDataUrl As String = String.Format(M_SrvIP &
                              "/rest/entities/{0}/{1}?dynamicAttributes=true{2}", Pr_FuncName, Pr_ID, Pr_Dm_par1)
            My_httpClient.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _MyTokenData)

            Dim response As HttpResponseMessage = Await My_httpClient.GetAsync(requestDataUrl)

            If response.IsSuccessStatusCode Then
                rt_vl55.Rt_txt = Await response.Content.ReadAsStringAsync()
                rt_vl55.Rt_Flg = True
            Else
                rt_vl55.Rt_txt = "API-დან მონაცემების მოძიება ვერ მოხერხდა. სტატუსის კოდი: " & response.StatusCode.ToString()
            End If
        Catch ex As Exception
            rt_vl55.Rt_txt = "დაშვებულია შეცდომა: " & ex.Message
        End Try
        Return rt_vl55
    End Function

    Public Async Function JS_DeleteCurentID(Pr_FuncName As String, Pr_ID As String) As Task(Of Rt_Class)
        Dim Ret_01 As New Rt_Class
        Try
            Dim rt_token As Rt_Class = Await JS_Get_TokenAsync(My_httpClient)
            If Not rt_token.Rt_flg Then
                Ret_01.Rt_txt = "წვდომის ტოკენის მიღება ვერ მოხერხდა! " & rt_token.Rt_txt
                Return Ret_01
            End If

            Dim requestDataUrl As String = String.Format(M_SrvIP &
                              "/rest/entities/{0}/{1}?dynamicAttributes=true", Pr_FuncName, Pr_ID)
            My_httpClient.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _MyTokenData)

            Dim response As HttpResponseMessage = Await My_httpClient.DeleteAsync(requestDataUrl)
            If response.IsSuccessStatusCode Then
                Ret_01.Rt_flg = True
                Ret_01.Rt_txt = Await response.Content.ReadAsStringAsync()
            Else
                Ret_01.Rt_txt = "API-დან მონაცემების მოძიება ვერ მოხერხდა. სტატუსის კოდი: " & response.StatusCode.ToString()
            End If
        Catch ex As Exception
            Ret_01.Rt_txt = "დაშვებულია შეცდომა: " & ex.Message
        End Try
        Return Ret_01
    End Function

    Public Async Function JS_DownloadCheckSumData(Pr_02 As String) As Task(Of Rt_Class)
        Dim rtString_01 As New Rt_Class
        rtString_01.Rt_txt = ""
        Try
            Dim rt_token As Rt_Class = Await JS_Get_TokenAsync(My_httpClient)
            If Not rt_token.Rt_flg Then
                rtString_01.Rt_txt = "წვდომის ტოკენის მიღება ვერ მოხერხდა! " & rt_token.Rt_txt
                Return rtString_01
            End If
            Dim requestDataUrl As String = String.Format(M_SrvIP &
                                                         "/rest/services/prx_CheckSum/calc")
            My_httpClient.DefaultRequestHeaders.Authorization =
                New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _MyTokenData)

            Dim content As New StringContent(Pr_02, Encoding.UTF8, "application/json")
            Dim response As HttpResponseMessage = Await My_httpClient.PostAsync(requestDataUrl, content)
            If response.IsSuccessStatusCode Then
                Dim rt_txt2 As String = Await response.Content.ReadAsStringAsync()
                Dim jsonToken2 As JToken = JsonConvert.DeserializeObject(Of JArray)(rt_txt2)
                If jsonToken2.Type = JTokenType.Array Then
                    rtString_01.Rt_Dt = JsonConvert.DeserializeObject(Of DataTable)(jsonToken2.ToString())
                    rtString_01.Rt_flg = True
                Else
                    rtString_01.Rt_txt = "ტექსტი არ შეიცავს JTokenType.Array ტიპის მონაცემებს"
                End If
            Else
                rtString_01.Rt_txt = " ფუნქცია ვერ შესრულდა. სტატუსის კოდი: " & response.StatusCode.ToString()
            End If
        Catch ex As Exception
            rtString_01.Rt_txt = "დაშვებულია შეცდომა: " & ex.Message
        End Try
        Return rtString_01
    End Function

    Public Async Function JS_DownloadFindData(Pr_FuncName As String, Pr_02 As String, Optional par2 As String = "") As Task(Of Rt_Class)
        Dim rtString_01 As New Rt_Class
        Try
            Dim rt_token As Rt_Class = Await JS_Get_TokenAsync(My_httpClient)
            If Not rt_token.Rt_flg Then
                rtString_01.Rt_txt = "წვდომის ტოკენის მიღება ვერ მოხერხდა! " & rt_token.Rt_txt
                Return rtString_01
            End If
            Dim requestDataUrl As String = String.Format(M_SrvIP &
                                                         "/rest/entities/{0}/search", Pr_FuncName)
            My_httpClient.DefaultRequestHeaders.Authorization =
                New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _MyTokenData)

            Dim content As New StringContent(Pr_02, Encoding.UTF8, "application/json")
            Dim response As HttpResponseMessage = Await My_httpClient.PostAsync(requestDataUrl, content)
            If response.IsSuccessStatusCode Then
                Dim rt_txt2 As String = Await response.Content.ReadAsStringAsync()
                Dim jsonToken2 As JToken = JsonConvert.DeserializeObject(Of JArray)(rt_txt2)
                If jsonToken2.Type = JTokenType.Array Then
                    rtString_01.Rt_Dt = JsonConvert.DeserializeObject(Of DataTable)(jsonToken2.ToString())
                    rtString_01.Rt_flg = True
                Else
                    rtString_01.Rt_txt = "ტექსტი არ შეიცავს JTokenType.Array ტიპის მონაცემებს"
                End If
            Else
                rtString_01.Rt_txt = Pr_FuncName & " ფუნქცია ვერ შესრულდა. სტატუსის კოდი: " & response.StatusCode.ToString()
            End If
        Catch ex As Exception
            rtString_01.Rt_txt = "დაშვებულია შეცდომა: " & ex.Message
        End Try
        Return rtString_01
    End Function

    Public Async Function JS_CreateData(Pr_FuncName As String, Pr_JsonString As String,
                                        Optional Pr_params As String = "") As Task(Of Rt_Class)
        Dim rtString_01 As New Rt_Class
        Try
            Dim rt_token As Rt_Class = Await JS_Get_TokenAsync(My_httpClient)
            If Not rt_token.Rt_flg Then
                rtString_01.Rt_txt = "წვდომის ტოკენის მიღება ვერ მოხერხდა! " & rt_token.Rt_txt
                Return rtString_01
            End If

            Dim requestDataUrl As String = M_SrvIP & "/rest/entities/" & Pr_FuncName & Pr_params
            My_httpClient.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _MyTokenData)

            Dim content As New StringContent(Pr_JsonString, Encoding.UTF8, "application/json")

            Dim response As HttpResponseMessage = Await My_httpClient.PostAsync(requestDataUrl, content)

            If response.IsSuccessStatusCode Then
                rtString_01.Rt_txt = Await response.Content.ReadAsStringAsync()
                rtString_01.Rt_flg = True
            Else
                rtString_01.Rt_txt = Pr_FuncName & " ფუნქცია ვერ შესრულდა. სტატუსის კოდი: " & response.StatusCode.ToString()
            End If
        Catch ex As Exception
            rtString_01.Rt_txt = "დაშვებულია შეცდომა: " & ex.Message
        End Try
        Return rtString_01
    End Function

    Public Async Function CreateData(Pr_FuncName As String, Pr_JsonString As String) As Task(Of Rt_Class)
        Dim rt_01 As New Rt_Class
        Try
            Dim rt_token As Rt_Class = Await JS_Get_TokenAsync(My_httpClient)
            If Not rt_token.Rt_flg Then
                rt_01.Rt_txt = "წვდომის ტოკენის მიღება ვერ მოხერხდა! " & rt_token.Rt_txt
                Return rt_01
            End If

            Dim requestDataUrl As String = M_SrvIP & "/rest/entities/" & Pr_FuncName
            My_httpClient.DefaultRequestHeaders.Authorization =
                New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _MyTokenData)

            Dim content As New StringContent(Pr_JsonString, Encoding.UTF8, "application/json")

            Dim response As HttpResponseMessage = Await My_httpClient.PostAsync(requestDataUrl, content)

            If response.IsSuccessStatusCode Then
                rt_01.Rt_txt = Await response.Content.ReadAsStringAsync()
                rt_01.Rt_flg = True
            Else
                rt_01.Rt_txt = Pr_FuncName & " ფუნქცია ვერ შესრულდა. სტატუსის კოდი: " & response.StatusCode.ToString()
            End If
        Catch ex As Exception
            rt_01.Rt_txt = "დაშვებულია შეცდომა: " & ex.Message
        End Try
        Return rt_01
    End Function

    Public Async Function CreateDataBl(Pr_FuncName As String, Pr_JsonString As String) As Task(Of Rt_Class)
        Dim rt_01 As New Rt_Class
        Try
            Dim rt_token As Rt_Class = Await JS_Get_TokenAsync(My_httpClient)
            If Not rt_token.Rt_flg Then
                rt_01.Rt_txt = "წვდომის ტოკენის მიღება ვერ მოხერხდა! " & rt_token.Rt_txt
                Return rt_01
            End If

            Dim requestDataUrl As String = M_SrvIP & "/rest/entities/" & Pr_FuncName
            My_httpClient.DefaultRequestHeaders.Authorization =
                New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _MyTokenData)

            Dim content As New StringContent(Pr_JsonString, Encoding.UTF8, "application/json")

            Dim response As HttpResponseMessage = Await My_httpClient.PostAsync(requestDataUrl, content)

            If response.IsSuccessStatusCode Then
                rt_01.Rt_flg = True
                rt_01.Rt_txt = Await response.Content.ReadAsStringAsync()
            Else
                rt_01.Rt_txt = Pr_FuncName & " ფუნქცია ვერ შესრულდა. სტატუსის კოდი: " & response.StatusCode.ToString()
            End If
        Catch ex As Exception
            rt_01.Rt_txt = "დაშვებულია შეცდომა: " & ex.Message
        End Try
        Return rt_01
    End Function

    Public Sub Err_MS_Sqlmsg(ByVal sender As Object, ByVal flg_MSG_View As Boolean, ByVal p_Class_name As String,
                             ByVal p_proc_name As String, ByVal cr_Ex As SqlException,
                             Optional cr_shen1 As String = "", Optional cr_shen2 As String = "")
        Try
            If flg_MSG_View Then
                MsgBox(cr_Ex.Message & vbCrLf & cr_Ex.StackTrace)
            End If
        Catch
        End Try
        Dim ob_nm22 As String = ""
        If sender = Nothing Then
            ob_nm22 = ""
            Try
                ob_nm22 = IIf(ob_nm22.Trim.Length > 0, ".", "")
            Catch
            End Try
        Else
            Try
                ob_nm22 = sender.name
            Catch
            End Try
            Try
                ob_nm22 = IIf(ob_nm22.Trim.Length > 0, ".", "") & sender.GetType.ToString
            Catch
            End Try

        End If
        Try
            Dim cnt_tmp1 As New SqlConnection(SqlClient_Tmp_con_string)
            cnt_tmp1.Open()
            Try
                Dim tsql_txt As String = "INSERT INTO dbo.tmp_error (err_txt,StackTrace_txt,Pcname,PcIP,ApiName,ProcedureName,ObjectName,shen1,shen2) " &
                                              "VALUES (@txt1,@txt2,@Pcname,@PcIP,@ApiName,@ProcedureName,@ObjectName,@shen1,@shen2)"
                Dim tDcd = New SqlCommand(tsql_txt, cnt_tmp1)
                tDcd.CommandType = CommandType.Text
                tDcd.Parameters.Add("@txt1", SqlDbType.NVarChar).Value = cr_Ex.Message
                tDcd.Parameters.Add("@txt2", SqlDbType.NVarChar).Value = cr_Ex.StackTrace
                tDcd.Parameters.Add("@Pcname", SqlDbType.NVarChar).Value = SystemInformation.ComputerName & "." & SystemInformation.UserName
                tDcd.Parameters.Add("@PcIP", SqlDbType.NVarChar).Value = System.Net.Dns.GetHostName
                tDcd.Parameters.Add("@ApiName", SqlDbType.NVarChar).Value = My.Application.Info.AssemblyName
                tDcd.Parameters.Add("@ProcedureName", SqlDbType.NVarChar).Value = IIf(p_Class_name.Trim.Length > 0, p_Class_name.Trim & ".", "") & p_proc_name
                tDcd.Parameters.Add("@ObjectName", SqlDbType.NVarChar).Value = ob_nm22
                tDcd.Parameters.Add("@shen1", SqlDbType.NVarChar).Value = cr_shen1
                tDcd.Parameters.Add("@shen2", SqlDbType.NVarChar).Value = cr_shen2

                tDcd.ExecuteNonQuery()
            Catch wx1 As Exception
                Dim a1 = 1
            Finally
                cnt_tmp1.Close()
            End Try
        Catch
        End Try
    End Sub

    Public Sub Err_Msg(ByVal sender As Object, ByVal flg_MSG_View As Boolean, ByVal p_Class_name As String, ByVal p_proc_name As String,
                      ByVal cr_Ex As Exception, Optional cr_shen1 As String = "", Optional cr_shen2 As String = "")
        Try
            If flg_MSG_View Then
                MsgBox(cr_Ex.Message & vbCrLf & cr_Ex.StackTrace)
            End If
        Catch
        End Try
        Dim ob_nm22 As String = ""
        If sender = Nothing Then
            ob_nm22 = ""
            Try
                ob_nm22 = IIf(ob_nm22.Trim.Length > 0, ".", "")
            Catch
            End Try
        Else
            Try
                ob_nm22 = sender.name
            Catch
            End Try
            Try
                ob_nm22 = IIf(ob_nm22.Trim.Length > 0, ".", "") & sender.GetType.ToString
            Catch
            End Try

        End If
        Try
            Dim cnt_tmp1 As New SqlConnection(SqlClient_Tmp_con_string)
            cnt_tmp1.Open()
            Try
                Dim tsql_txt As String = "INSERT INTO dbo.tmp_error (err_txt,StackTrace_txt,Pcname,PcIP,ApiName,ProcedureName,ObjectName,shen1,shen2) " &
                                              "VALUES (@txt1,@txt2,@Pcname,@PcIP,@ApiName,@ProcedureName,@ObjectName,@shen1,@shen2)"
                Dim tDcd = New SqlCommand(tsql_txt, cnt_tmp1)
                tDcd.CommandType = CommandType.Text
                tDcd.Parameters.Add("@txt1", SqlDbType.NVarChar).Value = cr_Ex.Message
                tDcd.Parameters.Add("@txt2", SqlDbType.NVarChar).Value = cr_Ex.StackTrace
                tDcd.Parameters.Add("@Pcname", SqlDbType.NVarChar).Value = SystemInformation.ComputerName & "." & SystemInformation.UserName
                tDcd.Parameters.Add("@PcIP", SqlDbType.NVarChar).Value = System.Net.Dns.GetHostName
                tDcd.Parameters.Add("@ApiName", SqlDbType.NVarChar).Value = My.Application.Info.AssemblyName
                tDcd.Parameters.Add("@ProcedureName", SqlDbType.NVarChar).Value = IIf(p_Class_name.Trim.Length > 0, p_Class_name.Trim & ".", "") & p_proc_name
                tDcd.Parameters.Add("@ObjectName", SqlDbType.NVarChar).Value = ob_nm22
                tDcd.Parameters.Add("@shen1", SqlDbType.NVarChar).Value = cr_shen1
                tDcd.Parameters.Add("@shen2", SqlDbType.NVarChar).Value = cr_shen2

                tDcd.ExecuteNonQuery()
            Catch wx1 As Exception
                Dim a1 = 1
            Finally
                cnt_tmp1.Close()
            End Try
        Catch
        End Try
    End Sub

    Public Sub Err_Sqlmsg(ByVal sender As Object, ByVal flg_MSG_View As Boolean, ByVal p_Class_name As String, ByVal p_proc_name As String,
                      ByVal cr_Ex As SqlException)
        Try
            If flg_MSG_View Then
                MsgBox(cr_Ex.Message & vbCrLf & cr_Ex.StackTrace)
            End If
        Catch
        End Try
        Dim ob_nm22 As String = ""
        If sender = Nothing Then
            ob_nm22 = ""
            Try
                ob_nm22 = IIf(ob_nm22.Trim.Length > 0, ".", "")
            Catch
            End Try
        Else
            Try
                ob_nm22 = sender.name
            Catch
            End Try
            Try
                ob_nm22 = IIf(ob_nm22.Trim.Length > 0, ".", "") & sender.GetType.ToString
            Catch
            End Try

        End If
        Try
            Dim cnt_tmp1 As New SqlConnection(SqlClient_Tmp_con_string)
            cnt_tmp1.Open()
            Try
                Dim tsql_txt As String = "INSERT INTO dbo.tmp_error (err_txt,StackTrace_txt,Pcname,PcIP,ApiName,ProcedureName,ObjectName) " &
                                              "VALUES (@txt1,@txt2,@Pcname,@PcIP,@ApiName,@ProcedureName,@ObjectName)"
                Dim tDcd = New SqlCommand(tsql_txt, cnt_tmp1)
                tDcd.CommandType = CommandType.Text
                tDcd.Parameters.Add("@txt1", SqlDbType.NVarChar).Value = cr_Ex.Message
                tDcd.Parameters.Add("@txt2", SqlDbType.NVarChar).Value = cr_Ex.StackTrace
                tDcd.Parameters.Add("@Pcname", SqlDbType.NVarChar).Value = SystemInformation.ComputerName & "." & SystemInformation.UserName
                tDcd.Parameters.Add("@PcIP", SqlDbType.NVarChar).Value = System.Net.Dns.GetHostName
                tDcd.Parameters.Add("@ApiName", SqlDbType.NVarChar).Value = My.Application.Info.AssemblyName
                tDcd.Parameters.Add("@ProcedureName", SqlDbType.NVarChar).Value = IIf(p_Class_name.Trim.Length > 0, p_Class_name.Trim & ".", "") & p_proc_name
                tDcd.Parameters.Add("@ObjectName", SqlDbType.NVarChar).Value = ob_nm22
                tDcd.ExecuteNonQuery()
            Catch wx1 As Exception
                Dim a1 = 1
            Finally
                cnt_tmp1.Close()
            End Try
        Catch
        End Try
    End Sub

    Public Function My_Save_Setting(ByVal mseqcia As String, ByVal mgankof As String, ByVal myKey As String, ByVal mValue As String) As Boolean
        If mseqcia = "" Then
            mseqcia = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name
        End If
        If mgankof = "" Then
            mgankof = "Admin"
        End If
        Try
            Dim CU As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\MGS\" & mseqcia & "\" & mgankof)
            With CU
                .OpenSubKey("SOFTWARE\MGS\" & mseqcia & "\" & mgankof, True)
                .SetValue(myKey, mValue)
            End With
            Return True
        Catch ex As Exception
            Dim a = 1
            Return False
        End Try
    End Function

    Public Function My_Get_Setting(ByVal mseqcia As String, ByVal mgankof As String, ByVal myKey As String, ByVal mValue As String) As String
        My_Get_Setting = mValue
        If mseqcia.Length = 0 Then
            mseqcia = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name
        End If
        If mgankof.Length = 0 Then
            mgankof = "Admin"
        End If
        Try
            Dim CU As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\MGS\" & mseqcia & "\" & mgankof)
            With CU
                .OpenSubKey("SOFTWARE\MGS\" & mseqcia & "\" & mgankof, True)
                My_Get_Setting = .GetValue(myKey, mValue)
            End With
        Catch
        End Try
    End Function

    Public Function ReadJsonArray(Pr_FuncName As String, Pr_cust_id As Integer) As Rt_Class
        Dim Rt_Val1 As New Rt_Class
        Dim Rt_DT1 As New DataTable
        Try
            Dim filePath As String = Application.StartupPath & $"\JsonFiles\JSon_Create_{Pr_FuncName}.txt"
            If My.Computer.FileSystem.DirectoryExists(Path.GetDirectoryName(filePath)) Then
                Dim fileContents As String = File.ReadAllText(filePath)
                Dim jsonToken1 As JToken = JsonConvert.DeserializeObject(Of JArray)(fileContents)
                If jsonToken1.Type = JTokenType.Array Then

                    Dim txt_JS As String = jsonToken1.ToString()
                    Rt_Val1.Rt_Dt = JsonConvert.DeserializeObject(Of DataTable)(txt_JS)
                    Rt_Val1.Rt_flg = True
                Else
                    Rt_Val1.Rt_txt = "ტექსტი არ შეიცავს JTokenType.Array ტიპის მონაცემებს"
                End If
            Else
                Rt_Val1.Rt_txt = "ფაილი ვერ მოინახა!"
            End If
        Catch ex As Exception

        End Try
        Rt_Val1.Rt_Dt = Rt_DT1
        Return Rt_Val1
    End Function

End Module

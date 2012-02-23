' 唯一のソースコード
' 
' パラメータ 
' itemData: (0)~(4)の間に、草,巻物,腕輪,壺,杖のデータ{名前,買値,売値,(買値の増分, 売値の増分)},
'           全種類の買値リスト,売値リスト,(杖の回数による買値リスト,売値リスト)が入っている
' 
' recognizedItem: 識別チェックボックスのステート 
' expectedItem: 予想チェックボックスのステート
' itemKeys: 全アイテムの名前
'



Imports System.Xml

Public Class Form1
    Private itemData(4) As ArrayList
    Private recognizedItem As New Hashtable()
    Private expectedItem As New Hashtable()
    Private itemKeys As New ArrayList()

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        prepareDataTable()
        prepareXmlData(My.Resources.Shiren2_item)
        itemTypeBox.SelectedItem = "草"
        remainNumBox.SelectedItem = "(0)"
        tradeTyepBox.SelectedItem = "買値"
        updatePriceBox()
    End Sub

    'テーブルの初期設定
    Private Sub prepareDataTable()
        dataTable.Columns.Add(New DataGridViewTextBoxColumn)
        dataTable.Columns.Add(New DataGridViewTextBoxColumn)
        dataTable.Columns.Add(New DataGridViewTextBoxColumn)
        dataTable.Columns.Add(New DataGridViewCheckBoxColumn)
        dataTable.Columns.Add(New DataGridViewCheckBoxColumn)
        dataTable.Columns(0).Name = "名前"
        dataTable.Columns(1).Name = "買値"
        dataTable.Columns(2).Name = "売値"
        dataTable.Columns(3).Name = "予想"
        dataTable.Columns(4).Name = "識別"
        dataTable.Columns(0).ReadOnly = True
        dataTable.Columns(1).ReadOnly = True
        dataTable.Columns(2).ReadOnly = True

        dataTable.Columns(0).Width *= 3.8 / 2.0
        dataTable.Columns(3).Width *= 1.1 / 2.0
        dataTable.Columns(4).Width *= 1.1 / 2.0
    End Sub

    'XMLデータの読み込み
    Private Sub prepareXmlData(ByVal xmlString As String)
        Dim doc As XmlDocument = New XmlDocument()
        doc.LoadXml(xmlString)
        prepareData(doc, "kusa", 0)
        prepareData(doc, "makimono", 1)
        prepareData(doc, "udewa", 2)
        prepareData(doc, "tubo", 3)
        prepareData(doc, "tue", 4)
    End Sub

    '各データの読み込み
    Private Sub prepareData(ByRef doc As XmlDocument, ByVal itemTypeTag As String, ByRef itemTypeNum As Integer)

        Dim list As XmlNodeList = doc.SelectNodes("/Shiren2/" + itemTypeTag + "/item")
        Dim itemNode As XmlNode
        Dim dataList As New ArrayList()
        For Each itemNode In list
            Dim itemData As New SortedList()
            itemData.Add(0, itemNode.SelectSingleNode("name").InnerText)
            itemData.Add(1, itemNode.SelectSingleNode("buyPrice").InnerText)
            itemData.Add(2, itemNode.SelectSingleNode("sellPrice").InnerText)
            If itemTypeNum >= 3 Then '壺,杖の場合
                itemData.Add(3, itemNode.SelectSingleNode("buyStep").InnerText)
                itemData.Add(4, itemNode.SelectSingleNode("sellStep").InnerText)
            End If
            dataList.Add(itemData)
            recognizedItem.Add(itemData(0), False)
            expectedItem.Add(itemData(0), False)
            itemKeys.Add(itemData(0))
        Next

        Dim MetaData As New ArrayList()
        MetaData.Add(dataList) 'アイテムデータ
        MetaData.Add(getPriceList(dataList, 1, itemTypeNum)) '買値リスト
        MetaData.Add(getPriceList(dataList, 2, itemTypeNum)) '売値リスト
        If itemTypeNum = 4 Then '杖の場合
            MetaData.Add(getTueAllTable(dataList, 1)) '買値テーブル
            MetaData.Add(getTueAllTable(dataList, 2)) '売値テーブル
        End If

        itemData(itemTypeNum) = MetaData
    End Sub

    '価格リストの取得
    Private Function getPriceList(ByRef data As ArrayList, ByVal tradeType As Integer, ByVal itemTypeNum As Integer) As ArrayList
        Dim priceList As New ArrayList()

        If itemTypeNum < 3 Then '草,巻物,腕輪の場合
            For Each item As SortedList In data
                If Not priceList.Contains(CInt(item(tradeType))) Then
                    priceList.Add(CInt(item(tradeType)))
                End If
            Next
            priceList.Sort()
            priceList.Insert(0, "ALL")
            Return priceList
        Else
            Dim maxRemain As Integer = 6
            If itemTypeNum = 4 Then  '杖の場合
                maxRemain = 7
            End If

            For i As Integer = 0 To maxRemain
                Dim perRemainList As New ArrayList()
                For Each item As SortedList In data
                    Dim incrementValue As Integer = Fix(CSng(item(tradeType + 2)) * i) '残数による価格調整
                    Dim price As Integer = CInt(item(tradeType)) + incrementValue
                    If Not perRemainList.Contains(price) Then
                        perRemainList.Add(price)
                    End If
                Next
                perRemainList.Sort()
                perRemainList.Insert(0, "ALL")
                priceList.Add(perRemainList)
            Next

            Return priceList
        End If
    End Function

    '杖の全検索用のリスト取得
    Private Function getTueAllTable(ByRef data As ArrayList, ByVal tradeType As Integer) As Hashtable
        Dim tueAllTable As New Hashtable
        For i As Integer = 0 To 7
            For Each item As SortedList In data
                Dim buyIncrementValue As Integer = Fix(CSng(item(3)) * i) '残数による価格調整
                Dim buyPrice As Integer = CInt(item(1)) + buyIncrementValue

                Dim sellIncrementValue As Integer = Fix(CSng(item(4)) * i)
                Dim sellPrice As Integer = CInt(item(2)) + sellIncrementValue

                Dim price As Integer
                If tradeType = 1 Then
                    price = buyPrice
                Else
                    price = sellPrice
                End If

                If Not tueAllTable.ContainsKey(CStr(price)) Then
                    Dim array As New ArrayList()
                    array.Add({CStr(item(0)) + "(" + CStr(i) + ")", CStr(buyPrice), CStr(sellPrice)})
                    tueAllTable.Add(CStr(price), array)
                Else
                    Dim array As ArrayList = tueAllTable.Item(CStr(price))
                    array.Add({CStr(item(0)) + "(" + CStr(i) + ")", CStr(buyPrice), CStr(sellPrice)})
                End If
            Next
        Next
        Return tueAllTable
    End Function

    '価格リストの更新
    Private Sub updatePriceBox()
        Dim metaData As ArrayList = itemData(itemTypeBox.SelectedIndex)
        If itemTypeBox.SelectedIndex < 3 Then '草,巻物,腕輪
            priceBox.DataSource = metaData(tradeTyepBox.SelectedIndex + 1)
            remainNumBox.Visible = False
        Else
            '壺で(7),ALLを選択してしまわないように
            If itemTypeBox.SelectedIndex = 3 AndAlso remainNumBox.SelectedIndex > 6 Then
                remainNumBox.SelectedIndex = 6
            End If

            Dim perPriceList As ArrayList = metaData(tradeTyepBox.SelectedIndex + 1)

            If remainNumBox.SelectedIndex = 8 Then
                priceBox.DataSource = New ArrayList()
                executeQuery()
            Else
                priceBox.DataSource = perPriceList(remainNumBox.SelectedIndex)
            End If

            '杖の時だけ(7),ALLを用意
            If itemTypeBox.SelectedIndex = 4 AndAlso Not remainNumBox.Items.Contains("(7)") Then
                remainNumBox.Items.Add("(7)")
                remainNumBox.Items.Add("ALL")
            ElseIf itemTypeBox.SelectedIndex = 3 AndAlso remainNumBox.Items.Contains("(7)") Then
                remainNumBox.Items.Remove("ALL")
                remainNumBox.Items.Remove("(7)")
            End If

            remainNumBox.Visible = True
        End If
    End Sub

    '検索
    Private Sub executeQuery()
        '数字列とオーバーフローの検査
        If Not IsNumeric(priceBox.Text) OrElse priceBox.Text.Length > 5 Then
            If priceBox.Text = "ALL" AndAlso itemTypeBox.SelectedIndex < 4 OrElse remainNumBox.SelectedIndex < 8 Then
                priceBox.SelectedIndex = 0
            Else
                priceBox.Text = "Err"
                dataTable.Rows.Clear()
                Return
            End If
        End If
        dataTable.Rows.Clear()

        '杖のALL検索時
        If itemTypeBox.SelectedIndex = 4 AndAlso remainNumBox.SelectedIndex = 8 Then
            Dim MetaData As ArrayList = itemData(4)
            Dim tueAllTable As Hashtable = MetaData(tradeTyepBox.SelectedIndex + 3)
            If tueAllTable.ContainsKey(priceBox.Text) Then
                For Each row As String() In tueAllTable(priceBox.Text)
                    Dim splitName As String() = Split(row(0), "(")
                    dataTable.Rows.Add(row(0), row(1), row(2), expectedItem.Item(splitName(0)), recognizedItem.Item(splitName(0)))
                Next
            End If
            Return
        End If

        Dim data As SortedList
        For Each data In itemData(itemTypeBox.SelectedIndex)(0)
            If itemTypeBox.SelectedIndex < 3 Then '草,巻物,腕輪
                If priceBox.SelectedIndex = 0 OrElse CInt(data.GetByIndex(tradeTyepBox.SelectedIndex + 1)) = CInt(priceBox.Text) Then

                    dataTable.Rows.Add(data.GetByIndex(0), data.GetByIndex(1), data.GetByIndex(2), _
                                       expectedItem.Item(data.GetByIndex(0)), recognizedItem.Item(data.GetByIndex(0)))
                End If
            Else
                Dim incrementValue As Integer = Fix(CSng(data.GetByIndex(tradeTyepBox.SelectedIndex + 3)) * remainNumBox.SelectedIndex)
                Dim price As Integer = CInt(data.GetByIndex(tradeTyepBox.SelectedIndex + 1)) + incrementValue
                If priceBox.SelectedIndex = 0 OrElse price = CInt(priceBox.Text) Then
                    Dim buyIncrementValue As Integer = Fix(CSng(data.GetByIndex(3)) * remainNumBox.SelectedIndex)
                    Dim buyPrice As Integer = CInt(data.GetByIndex(1)) + buyIncrementValue
                    Dim sellIncrementValue As Integer = Fix(CSng(data.GetByIndex(4)) * remainNumBox.SelectedIndex)
                    Dim sellPrice As Integer = CInt(data.GetByIndex(2)) + sellIncrementValue
                    dataTable.Rows.Add(data.GetByIndex(0), buyPrice, sellPrice, _
                                       expectedItem.Item(data.GetByIndex(0)), recognizedItem.Item(data.GetByIndex(0)))
                End If
            End If
        Next
    End Sub



    'アイテムの種類選択時
    Private Sub itemTypeBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles itemTypeBox.SelectedIndexChanged
        updatePriceBox()
    End Sub

    '壺,杖の残数選択時
    Private Sub remainNumBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles remainNumBox.SelectedIndexChanged
        updatePriceBox()
    End Sub

    '取引形式選択時
    Private Sub tradeTypeBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tradeTyepBox.SelectedIndexChanged
        updatePriceBox()
    End Sub

    '価格選択時
    Private Sub priceBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles priceBox.SelectedIndexChanged
        executeQuery()
    End Sub

    '価格入力時
    Private Sub priceBox_PushedEnterKey(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles priceBox.KeyUp
        If e.KeyCode = Keys.Enter Then
            executeQuery()
        End If
    End Sub

    'チェックボックス選択時
    Private Sub dataTable_CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dataTable.CellValueChanged
        '列のインデックスを確認する
        If e.ColumnIndex = 3 Then
            If itemTypeBox.SelectedIndex = 4 AndAlso remainNumBox.SelectedIndex = 8 Then
                Dim splitName As String() = Split(CStr(dataTable(0, e.RowIndex).Value), "(")
                expectedItem.Item(splitName(0)) = dataTable(e.ColumnIndex, e.RowIndex).Value
            Else
                expectedItem.Item(CStr(dataTable(0, e.RowIndex).Value)) = dataTable(e.ColumnIndex, e.RowIndex).Value
            End If
        ElseIf e.ColumnIndex Then
            If itemTypeBox.SelectedIndex = 4 AndAlso remainNumBox.SelectedIndex = 8 Then
                Dim splitName As String() = Split(CStr(dataTable(0, e.RowIndex).Value), "(")
                recognizedItem.Item(splitName(0)) = dataTable(e.ColumnIndex, e.RowIndex).Value
            Else
                recognizedItem.Item(CStr(dataTable(0, e.RowIndex).Value)) = dataTable(e.ColumnIndex, e.RowIndex).Value
            End If

        End If
    End Sub


    '数値ソートを可能にする
    Private Sub dataTable_SortCompare(ByVal sender As Object, ByVal e As DataGridViewSortCompareEventArgs) Handles dataTable.SortCompare

        If e.Column.Index = 1 OrElse e.Column.Index = 2 Then
            e.SortResult = CInt(e.CellValue1) - CInt(e.CellValue2)
            e.Handled = True
        End If

    End Sub

    '解除ボタン選択時
    Private Sub resetButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles removeButton.Click
        askCheckRemove()
    End Sub

    'チェックボックスの解除
    Private Sub askCheckRemove()
        Dim result As DialogResult = MessageBox.Show("チェックボックスをすべて解除しますか？", _
                                             "質問", _
                                             MessageBoxButtons.YesNo, _
                                             MessageBoxIcon.Exclamation, _
                                             MessageBoxDefaultButton.Button2)
        If result = DialogResult.Yes Then
            For Each key As String In itemKeys
                expectedItem.Item(key) = False
                recognizedItem.Item(key) = False
            Next
            executeQuery()
        End If

    End Sub

End Class
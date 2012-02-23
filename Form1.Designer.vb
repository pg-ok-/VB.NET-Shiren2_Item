<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.itemTypeBox = New System.Windows.Forms.ComboBox()
        Me.priceBox = New System.Windows.Forms.ComboBox()
        Me.dataTable = New System.Windows.Forms.DataGridView()
        Me.tradeTyepBox = New System.Windows.Forms.ComboBox()
        Me.remainNumBox = New System.Windows.Forms.ComboBox()
        Me.removeButton = New System.Windows.Forms.Button()
        CType(Me.dataTable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'itemTypeBox
        '
        Me.itemTypeBox.BackColor = System.Drawing.SystemColors.Window
        Me.itemTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.itemTypeBox.FormattingEnabled = True
        Me.itemTypeBox.Items.AddRange(New Object() {"草", "巻物", "腕輪", "壺", "杖"})
        Me.itemTypeBox.Location = New System.Drawing.Point(22, 23)
        Me.itemTypeBox.Name = "itemTypeBox"
        Me.itemTypeBox.Size = New System.Drawing.Size(48, 20)
        Me.itemTypeBox.TabIndex = 1
        '
        'priceBox
        '
        Me.priceBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.priceBox.FormattingEnabled = True
        Me.priceBox.Location = New System.Drawing.Point(239, 23)
        Me.priceBox.Name = "priceBox"
        Me.priceBox.Size = New System.Drawing.Size(66, 20)
        Me.priceBox.TabIndex = 2
        '
        'dataTable
        '
        Me.dataTable.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.dataTable.AllowUserToAddRows = False
        Me.dataTable.AllowUserToDeleteRows = False
        Me.dataTable.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dataTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dataTable.BackgroundColor = System.Drawing.Color.DarkGray
        Me.dataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataTable.Location = New System.Drawing.Point(22, 59)
        Me.dataTable.MultiSelect = False
        Me.dataTable.Name = "dataTable"
        Me.dataTable.RowTemplate.Height = 21
        Me.dataTable.Size = New System.Drawing.Size(287, 268)
        Me.dataTable.TabIndex = 3
        '
        'tradeTyepBox
        '
        Me.tradeTyepBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tradeTyepBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tradeTyepBox.FormattingEnabled = True
        Me.tradeTyepBox.Items.AddRange(New Object() {"買値", "売値"})
        Me.tradeTyepBox.Location = New System.Drawing.Point(169, 23)
        Me.tradeTyepBox.Name = "tradeTyepBox"
        Me.tradeTyepBox.Size = New System.Drawing.Size(54, 20)
        Me.tradeTyepBox.TabIndex = 4
        '
        'remainNumBox
        '
        Me.remainNumBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.remainNumBox.FormattingEnabled = True
        Me.remainNumBox.Items.AddRange(New Object() {"(0)", "(1)", "(2)", "(3)", "(4)", "(5)", "(6)"})
        Me.remainNumBox.Location = New System.Drawing.Point(85, 23)
        Me.remainNumBox.Name = "remainNumBox"
        Me.remainNumBox.Size = New System.Drawing.Size(44, 20)
        Me.remainNumBox.TabIndex = 5
        Me.remainNumBox.Visible = False
        '
        'removeButton
        '
        Me.removeButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.removeButton.Location = New System.Drawing.Point(22, 334)
        Me.removeButton.Name = "removeButton"
        Me.removeButton.Size = New System.Drawing.Size(66, 23)
        Me.removeButton.TabIndex = 6
        Me.removeButton.Text = "解除"
        Me.removeButton.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(338, 369)
        Me.Controls.Add(Me.removeButton)
        Me.Controls.Add(Me.remainNumBox)
        Me.Controls.Add(Me.tradeTyepBox)
        Me.Controls.Add(Me.dataTable)
        Me.Controls.Add(Me.priceBox)
        Me.Controls.Add(Me.itemTypeBox)
        Me.MinimumSize = New System.Drawing.Size(346, 250)
        Me.Name = "Form1"
        Me.Text = "シレン2 アイテム鑑定屋"
        CType(Me.dataTable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents itemTypeBox As System.Windows.Forms.ComboBox
    Friend WithEvents priceBox As System.Windows.Forms.ComboBox
    Friend WithEvents dataTable As System.Windows.Forms.DataGridView
    Friend WithEvents tradeTyepBox As System.Windows.Forms.ComboBox
    Friend WithEvents remainNumBox As System.Windows.Forms.ComboBox
    Friend WithEvents removeButton As System.Windows.Forms.Button
End Class

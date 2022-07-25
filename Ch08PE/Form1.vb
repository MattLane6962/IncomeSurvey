
' Programmer:       Matthew Lane
' Date:             4/21/2016
' Description:      Program analyses a income survey, includes an identification code
'                   number of members in a household, and the yearly income.



Public Class Form1
    ' Declare a structure.
    Structure IncomeStructure
        Dim IDNumberInteger As Integer
        Dim NumOfPersonsInteger As Integer
        Dim AnnualIncomeDecimal As Decimal
    End Structure

    ' Declare variables.
    Private CountInteger As Integer = 1
    Private Income(50) As IncomeStructure
    Private TotalDecimal As Decimal = 0
    Private AverageDecimal As Decimal


    Private Sub EnterDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnterDataToolStripMenuItem.Click
        ' Allows entry of income data.

        IDCodeTextBox.Enabled = True
        NoOfPersonsTextBox.Enabled = True
        YearlyIncomeTextBox.Enabled = True
        IDCodeTextBox.Focus()
        EnterDataToolStripMenuItem.Enabled = True
        ClearButton.Enabled = True
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        ' Closes the program.

        Me.Close()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        ' Displays the about box.

        AboutBox1.ShowDialog()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ' Input data to print.

        Dim PrintFont As New Font("Arial", 30)
        Dim MyFont As New Font("Arial", 34)
        Dim LineHeightSingle As Single = PrintFont.GetHeight + 3
        Dim HorizontalPrintLocation As Single = e.MarginBounds.Left
        Dim VerticalPrintLocation As Single = e.MarginBounds.Top + 150
        Dim s1, s2, s3 As String

        e.Graphics.DrawString("Input Data Report", MyFont, Brushes.Black, 100, 100)
        e.Graphics.DrawString("ID", PrintFont, Brushes.Black,
            HorizontalPrintLocation, VerticalPrintLocation)
        e.Graphics.DrawString("Persons ", PrintFont, Brushes.Black,
                HorizontalPrintLocation + 200, VerticalPrintLocation)
        e.Graphics.DrawString("Income ", PrintFont, Brushes.Black,
            HorizontalPrintLocation + 400, VerticalPrintLocation)

        VerticalPrintLocation += LineHeightSingle

        For cnt As Integer = 1 To CountInteger - 1
            s1 = Income(cnt).IDNumberInteger
            s2 = Income(cnt).NumOfPersonsInteger
            s3 = Income(cnt).AnnualIncomeDecimal

            e.Graphics.DrawString(s1, PrintFont, Brushes.Black, HorizontalPrintLocation,
                VerticalPrintLocation)
            e.Graphics.DrawString(s2, PrintFont, Brushes.Black, HorizontalPrintLocation + 200,
                    VerticalPrintLocation)
            e.Graphics.DrawString(s3, PrintFont, Brushes.Black, HorizontalPrintLocation + 400,
                                VerticalPrintLocation)
        Next


    End Sub

    Private Sub InputDataReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InputDataReportToolStripMenuItem.Click
        ' Displays data input for printing.

        PrintPreviewDialog1 = New PrintPreviewDialog
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Sub PrintDocument2_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument2.PrintPage
        ' Average income exceeded details.

        Dim n As Integer
        Dim PrintFont As New Font("Arial", 30)
        Dim LineHeightSingle As Single = PrintFont.GetHeight + 3
        Dim HorizontalPrintLocation As Single = e.MarginBounds.Left
        Dim VerticalPrintLocation As Single = e.MarginBounds.Top + 150
        Dim MyFont As New Font("Arial", 32)
        Dim s1, s2, s3 As String

        n = CountInteger - 1

        If n > 1 Then
            AverageDecimal = TotalDecimal / n
        End If

        e.Graphics.DrawString("Average Income Exceeded Report", MyFont, Brushes.Black, 100, 100)
        e.Graphics.DrawString("ID", PrintFont, Brushes.Black, HorizontalPrintLocation, VerticalPrintLocation)
        e.Graphics.DrawString("Persons", PrintFont, Brushes.Black, HorizontalPrintLocation + 200,
                              VerticalPrintLocation)
        e.Graphics.DrawString("Income", PrintFont, Brushes.Black, HorizontalPrintLocation + 400,
                              VerticalPrintLocation)

        VerticalPrintLocation += LineHeightSingle

        For j As Integer = 1 To CountInteger - 1

            If (Income(j).AnnualIncomeDecimal > AverageDecimal) Then
                s1 = Income(j).IDNumberInteger
                s2 = Income(j).NumOfPersonsInteger
                s3 = Income(j).AnnualIncomeDecimal

                e.Graphics.DrawString(s1, PrintFont, Brushes.Black, HorizontalPrintLocation,
                                      VerticalPrintLocation)
                e.Graphics.DrawString(s2, PrintFont, Brushes.Black, HorizontalPrintLocation + 200,
                                      VerticalPrintLocation)
                e.Graphics.DrawString(s3, PrintFont, Brushes.Black, HorizontalPrintLocation + 400,
                                      VerticalPrintLocation)
                VerticalPrintLocation += LineHeightSingle
            End If
        Next
    End Sub

    Private Sub AverageIncomeExceededReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AverageIncomeExceededReportToolStripMenuItem.Click

        If PrintPreviewDialog1 Is Nothing Then
            PrintPreviewDialog1 = New PrintPreviewDialog
        End If

        PrintPreviewDialog1.Document = PrintDocument2
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Sub IncomeBelowPovertyReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IncomeBelowPovertyReportToolStripMenuItem.Click

        PrintPreviewDialog1.Document = PrintDocument3
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Sub YearlyIncomeTextBox_TextChanged(sender As Object, e As EventArgs) Handles YearlyIncomeTextBox.TextChanged
        ' Enables the submit button.

        SubmitButton.Enabled = True
    End Sub

    Private Sub PrintDocument3_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument3.PrintPage

        Dim PrintFont As New Font("Arial", 30)
        Dim LineHeight As Single = PrintFont.GetHeight + 2
        Dim HorizontalPrintLocation As Single = e.MarginBounds.Left
        Dim VerticalPrintLocation As Single = e.MarginBounds.Top + 150
        Dim temp As Integer
        Dim MyFont As New Font("Arial", 32)
        Dim percent As Decimal
        Dim pl As Integer

        e.Graphics.DrawString("Below poverty percentage", MyFont, Brushes.Black, 100, 100)

        For j As Integer = 1 To CountInteger - 1
            Select Case Income(j).NumOfPersonsInteger
                Case 1
                    If Income(j).AnnualIncomeDecimal < 10210 Then
                        pl += 1
                    End If
                Case 2
                    If Income(j).AnnualIncomeDecimal < 13690 Then
                        pl += 1
                    End If
                Case 3
                    If Income(j).AnnualIncomeDecimal < 17170 Then
                        pl += 1
                    End If
                Case 4
                    If Income(j).AnnualIncomeDecimal < 20650 Then
                        pl += 1
                    End If
                Case 5
                    If Income(j).AnnualIncomeDecimal < 24130 Then
                        pl += 1
                    End If
                Case 6
                    If Income(j).AnnualIncomeDecimal < 27610 Then
                        pl += 1
                    End If
                Case 7
                    If Income(j).AnnualIncomeDecimal < 31090 Then
                        pl += 1
                    End If
                Case 8
                    If Income(j).AnnualIncomeDecimal < 34570 Then
                        pl += 1
                    End If
            End Select
            If (Income(j).NumOfPersonsInteger > 8) Then
                temp = (Income(j).NumOfPersonsInteger - 8) * 3480 + 34570
                If Income(j).AnnualIncomeDecimal < temp Then
                    pl += 1
                End If
            End If
           
        Next

        percent = (pl / (CountInteger - 1)) * 100
        percent = percent.ToString()


        e.Graphics.DrawString(Environment.NewLine & percent & "%", PrintFont, Brushes.Black, HorizontalPrintLocation, VerticalPrintLocation)

    End Sub

    Private Sub SubmitButton_Click(sender As Object, e As EventArgs) Handles SubmitButton.Click
        ' Income of the household is added to the array of structure.

        Income(CountInteger).IdNumberInteger = Integer.Parse(IDCodeTextBox.Text)
        Income(CountInteger).NumOfPersonsInteger = Integer.Parse(NoOfPersonsTextBox.Text)
        Income(CountInteger).AnnualIncomeDecimal = Integer.Parse(YearlyIncomeTextBox.Text)

        MsgBox("Data Inserted")
        TotalDecimal += Income(CountInteger).AnnualIncomeDecimal

        IDCodeTextBox.Clear()
        NoOfPersonsTextBox.Clear()
        YearlyIncomeTextBox.Clear()
        IDCodeTextBox.Enabled = False
        NoOfPersonsTextBox.Enabled = False
        YearlyIncomeTextBox.Enabled = False
        InputDataReportToolStripMenuItem.Enabled = True
        AverageIncomeExceededReportToolStripMenuItem.Enabled = True
        IncomeBelowPovertyReportToolStripMenuItem.Enabled = True

        CountInteger += 1

        EnterDataToolStripMenuItem.Enabled = True
        SubmitButton.Enabled = False
    End Sub

    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        ' Clears the text boxes.

        IDCodeTextBox.Clear()
        NoOfPersonsTextBox.Clear()
        YearlyIncomeTextBox.Clear()
    End Sub
End Class



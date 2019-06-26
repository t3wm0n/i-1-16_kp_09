using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Words.NET;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Reports.xaml
    /// </summary>
    public partial class Reports : UserControl
    {
        public string extension;
        public string table;
        public string pathDocument = DB.docpath;

        public Reports()
        {
            InitializeComponent();
        }

        private void CreateReport_Click(object sender, RoutedEventArgs e)
        {
            if (table != "" && extension != "")
            {
                pathDocument += table + extension;
                switch (extension)
                {
                    case (".docx"):
                        DocX document = DocX.Create(pathDocument);
                        var fullr = DB.tae.FullReport.Where(r => r.Report_Date > date1.SelectedDate & r.Report_Date < date2.SelectedDate);
                        Paragraph paragraph = document.InsertParagraph();
                        paragraph.AppendLine("Документ '" + table + "' создан " + DateTime.Now.ToShortDateString()).Font("Times New Roman").FontSize(16).Bold().
                            Alignment = Alignment.left;
                        paragraph.AppendLine("Период: c " + date1.SelectedDate.ToString() + " по " + date2.SelectedDate.ToString()).Font("Times New Roman").FontSize(14).
                            Alignment = Alignment.left;
                        paragraph.AppendLine();
                        int j = 1, profit = 0;
                        Table doctable = document.AddTable(fullr.ToList().Count + 1, 6);
                        doctable.Design = TableDesign.TableGrid;
                        doctable.TableCaption = "Доходы и расходы";

                        doctable.Rows[0].Cells[0].Paragraphs[0].Append("Номер").Font("Times New Roman").FontSize(14);
                        doctable.Rows[0].Cells[1].Paragraphs[0].Append("Дата заказа").Font("Times New Roman").FontSize(14);
                        doctable.Rows[0].Cells[2].Paragraphs[0].Append("Расходы на заказ").Font("Times New Roman").FontSize(14);
                        doctable.Rows[0].Cells[3].Paragraphs[0].Append("Доход с заказа").Font("Times New Roman").FontSize(14);
                        doctable.Rows[0].Cells[4].Paragraphs[0].Append("Промокод").Font("Times New Roman").FontSize(14);
                        doctable.Rows[0].Cells[5].Paragraphs[0].Append("Стоимость заказа").Font("Times New Roman").FontSize(14);

                        for (int i = 0; i < doctable.RowCount - 1; i++)
                        {
                            FullReport fod = fullr.FirstOrDefault(f => f.ID_Report == j);
                            while (fod == null)
                            {
                                j++;
                                fod = fullr.FirstOrDefault(f => f.ID_Report == j);
                            }
                            doctable.Rows[i + 1].Cells[0].Paragraphs[0].Append(fod.ID_Report.ToString()).Font("Times New Roman").FontSize(14);
                            doctable.Rows[i + 1].Cells[1].Paragraphs[0].Append(fod.Report_Date.ToShortDateString()).Font("Times New Roman").FontSize(14);
                            doctable.Rows[i + 1].Cells[2].Paragraphs[0].Append(fod.Costs.ToString()).Font("Times New Roman").FontSize(14);
                            doctable.Rows[i + 1].Cells[3].Paragraphs[0].Append(fod.Profit.ToString()).Font("Times New Roman").FontSize(14);
                            doctable.Rows[i + 1].Cells[4].Paragraphs[0].Append(fod.Promocode).Font("Times New Roman").FontSize(14);
                            doctable.Rows[i + 1].Cells[5].Paragraphs[0].Append(fod.Order_Cost.ToString()).Font("Times New Roman").FontSize(14);
                            profit += fod.Profit;
                            j = i + 2;
                        }
                        document.InsertParagraph().InsertTableAfterSelf(doctable);
                        paragraph.AppendLine();
                        paragraph.AppendLine("Общий доход с заказов за выбранный период: " + profit.ToString()).Font("Times New Roman").FontSize(14).Alignment = Alignment.right;
                        document.Save();
                        MessageBox.Show("Отчёт успешно сформирован!");
                        Process.Start(pathDocument);
                        break;





                    case (".xlsx"):
                        var fulls = DB.tae.FullReport.Where(r => r.Report_Date > date1.SelectedDate & r.Report_Date < date2.SelectedDate);
                        int jj = 1, profitt = 0;
                        Excel.Application ex = new Excel.Application();
                        ex.SheetsInNewWorkbook = 1;
                        Excel.Workbook workbook = ex.Workbooks.Add(Type.Missing);
                        ex.DisplayAlerts = false;
                        Excel.Worksheet sheet = (Excel.Worksheet)ex.Worksheets.get_Item(1);
                        sheet.Name = table;

                        sheet.Cells[1, 1] = String.Format("Номер");
                        sheet.Cells[1, 2] = String.Format("Дата заказа");
                        sheet.Cells[1, 3] = String.Format("Расходы на заказ");
                        sheet.Cells[1, 4] = String.Format("Доход с заказа");
                        sheet.Cells[1, 5] = String.Format("Промокод");
                        sheet.Cells[1, 6] = String.Format("Стоимость заказа");

                        for (int i = 2; i < fulls.ToList().Count + 2; i++)
                        {
                            FullReport fod = fulls.FirstOrDefault(f => f.ID_Report == jj);
                            while (fod == null)
                            {
                                jj++;
                                fod = fulls.FirstOrDefault(f => f.ID_Report == jj);
                            }
                            sheet.Cells[i, 1] = String.Format(fod.ID_Report.ToString());
                            sheet.Cells[i, 2] = String.Format(fod.Report_Date.ToShortDateString());
                            sheet.Cells[i, 3] = String.Format(fod.Costs.ToString());
                            sheet.Cells[i, 4] = String.Format(fod.Profit.ToString());
                            sheet.Cells[i, 5] = String.Format(fod.Promocode);
                            sheet.Cells[i, 6] = String.Format(fod.Order_Cost.ToString());
                            profitt += fod.Profit;
                            jj = i;
                        }
                        sheet.Cells[1, 7] = String.Format("Общий доход с заказов за выбранный период: ");
                        sheet.Cells[1, 8] = String.Format(profitt.ToString());
                        Excel.Range rex = sheet.Cells[jj, 8] as Excel.Range;
                        rex.EntireColumn.AutoFit();
                        rex.EntireRow.AutoFit();
                        ex.Application.ActiveWorkbook.SaveAs(pathDocument, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        ex.Visible = true;
                        ex.Quit();
                        break;





                    case (".pdf"):
                        if (File.Exists(DB.docpath + table + ".docx"))
                        {
                            Word.Application appWord = new Word.Application();
                            var wordDocument = appWord.Documents.Open(DB.docpath + table + ".docx");
                            wordDocument.ExportAsFixedFormat(pathDocument, Word.WdExportFormat.wdExportFormatPDF);
                        }
                        else
                            MessageBox.Show("Сначала сформируйте отчет .docx");
                        break;
                }
                pathDocument = DB.docpath;
            }
        }

        private void Table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            table = Table.SelectedValue.ToString().Substring(38);
        }

        private void Ext_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            extension = Ext.SelectedValue.ToString().Substring(38);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            switch (extension)
            {
                case (".docx"):
                    pathDocument += table + extension;
                    DocX document = DocX.Create(pathDocument);
                    var fullr = DB.tae.FullReport.Where(r => r.Report_Date > date1.SelectedDate & r.Report_Date < date2.SelectedDate);
                    Paragraph paragraph = document.InsertParagraph();
                    paragraph.AppendLine("Документ '" + table + "' создан " + DateTime.Now.ToShortDateString()).Font("Times New Roman").FontSize(16).Bold().
                        Alignment = Alignment.left;
                    paragraph.AppendLine("Период: c " + date1.SelectedDate.ToString() + " по " + date2.SelectedDate.ToString()).Font("Times New Roman").FontSize(14).
                        Alignment = Alignment.left;
                    paragraph.AppendLine();
                    int j = 1, profit = 0;
                    Table doctable = document.AddTable(fullr.ToList().Count + 1,6);
                    doctable.Design = TableDesign.TableGrid;
                    doctable.TableCaption = "Доходы и расходы";
                    FullReport fr = new FullReport();

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
                    break;
                case (".xlsx"):
                    break;
                case (".pdf"):
                    break;
            }
            MessageBox.Show("Отчёт успешно сформирован!");
            Process.Start(pathDocument);
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

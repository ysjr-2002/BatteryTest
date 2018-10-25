using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.Excel
{
    public class ExcelHelper
    {
        static HSSFWorkbook book;
        static ICellStyle cellType_normal;
        static ICellStyle cellType_bold;

        public static bool ExportStat_CLPPTJ(List<string> list)
        {
            //打开保存excel对话框
            var filepath = SaveExcelFileDialog();
            if (filepath == null) return false;

            book = new HSSFWorkbook();

            cellType_normal = book.CreateCellStyle();
            cellType_normal.VerticalAlignment = VerticalAlignment.Center;
            cellType_normal.Alignment = HorizontalAlignment.Center;

            cellType_bold = book.CreateCellStyle();
            cellType_bold.VerticalAlignment = VerticalAlignment.Center;
            cellType_bold.Alignment = HorizontalAlignment.Center;

            IFont font = book.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;
            cellType_bold.SetFont(font);

            sheet1();

            sheet2();

            sheet3();

            SaveFile(book, filepath);
            return true;
        }

        private static void sheet1()
        {
            ISheet sheet = book.CreateSheet("Burn-in Report");
            sheet.SetColumnWidth(0, 3000);
            sheet.SetColumnWidth(1, 6000); //卡口名称可能比较长
            sheet.SetColumnWidth(2, 6000);
            sheet.SetColumnWidth(3, 4000);
            sheet.SetColumnWidth(4, 4000);
            sheet.SetColumnWidth(5, 4000);
            sheet.SetColumnWidth(6, 4000);
            sheet.SetColumnWidth(7, 4000);

            int rowIndex = 0;
            //第一行合并列
            AddHeader(sheet, "电源老化测试报表", 7);

            //第二行
            IRow row = CreateRow(sheet, 1);
            CreateCell(row, 0, "产品订单号", true);
            CreateCell(row, 1, "产品机型", true);
            CreateCell(row, 2, "老化时间", true);
            CreateCell(row, 3, "总数", true);
            CreateCell(row, 4, "良品", true);
            CreateCell(row, 5, "不良品", true);
            CreateCell(row, 6, "不良率", true);
            CreateCell(row, 7, "创建时间", true);

            //赋值
            row = CreateRow(sheet, 2, 300);
            CreateCell(row, 0, "123", false);
            CreateCell(row, 1, "L-235", false);
            CreateCell(row, 2, "00:23:00", false);
            CreateCell(row, 3, "100", false);
            CreateCell(row, 4, "98", false);
            CreateCell(row, 5, "2", false);
            CreateCell(row, 6, "2%", false);
            CreateCell(row, 7, "2018-10-23", false);

            rowIndex = 10;
            row = CreateRow(sheet, rowIndex);
            CreateCell(row, 0, "产品订单号", true);
            CreateCell(row, 1, "老化参数调用", true);
            CreateCell(row, 2, "判断范围", true);
            CreateCell(row, 3, "是否冲击", true);
            CreateCell(row, 4, "AC执行时间", true);
            CreateCell(row, 5, "AC关时长", true);
            CreateCell(row, 6, "AC开时长", true);

            rowIndex = 15;
            row = CreateRow(sheet, rowIndex);
            CreateCell(row, 0, "序号", true);
            CreateCell(row, 1, "产品位置", true);
            CreateCell(row, 2, "产品条形码", true);
            CreateCell(row, 3, "电压(V)", true);
            CreateCell(row, 4, "电流(A)", true);

            var cell5 = CreateCell(row, 5, "测试参数", true);
            var cell6 = CreateCell(row, 6, "", true);
            var cell7 = CreateCell(row, 7, "", true);

            var temp = new CellRangeAddress(rowIndex, rowIndex, 5, 7);
            sheet.AddMergedRegion(temp);

            CreateCell(row, 8, "Pass/Fail", true);
            CreateCell(row, 9, "Fail time", true);
        }

        private static void sheet2()
        {
            ISheet sheet = book.CreateSheet("Fail Report");
            sheet.SetColumnWidth(0, 3000);
            sheet.SetColumnWidth(1, 6000);
            sheet.SetColumnWidth(2, 6000);
            sheet.SetColumnWidth(3, 4000);
            sheet.SetColumnWidth(4, 4000);
            sheet.SetColumnWidth(5, 10000);
            sheet.SetColumnWidth(6, 4000);
            IRow row1 = CreateRow(sheet, 0);
            CreateCell(row1, 0, "产品位置", true);
            CreateCell(row1, 1, "记录时间", true);
            CreateCell(row1, 2, "输入电压", true);
            CreateCell(row1, 3, "电压(V)", true);
            CreateCell(row1, 4, "电流(A)", true);
            CreateCell(row1, 5, "测试参数", true);
            CreateCell(row1, 6, "备注", true);
        }

        private static void sheet3()
        {
            ISheet sheet = book.CreateSheet("Data records");
        }

        private static IRow CreateRow(ISheet sheet, int rowIndex, short height = 20 * 20)
        {
            IRow row = sheet.CreateRow(rowIndex);
            row.Height = height;
            return row;
        }

        private static ICell CreateCell(IRow row, int index, string value, bool bold)
        {
            var cell = row.CreateCell(index);
            if (bold)
                cell.CellStyle = cellType_bold;
            else
                cell.CellStyle = cellType_normal;
            cell.SetCellValue(value);
            return cell;
        }

        private static void AddHeader(ISheet sheet, string title, int cols)
        {
            IRow row = sheet.CreateRow(0);
            row.Height = 40 * 20;
            ICellStyle style = book.CreateCellStyle();
            style.VerticalAlignment = VerticalAlignment.Center;
            style.Alignment = HorizontalAlignment.Center;
            IFont font = book.CreateFont();
            font.FontHeightInPoints = 20;
            font.Boldweight = (short)FontBoldWeight.Bold;
            style.SetFont(font);
            var cell = row.CreateCell(0);
            cell.CellStyle = style;
            cell.SetCellValue(title);
            for (int i = 1; i <= cols; i++)
            {
                row.CreateCell(i).SetCellValue("");
            }
            var temp = new CellRangeAddress(0, 0, 0, cols);
            sheet.AddMergedRegion(temp);
        }

        public static string SaveExcelFileDialog()
        {
            var sfd = new Microsoft.Win32.SaveFileDialog()
            {
                DefaultExt = "xls",
                Filter = "Excel File(*.xls)|*.xls|Excel File(*.xlsx)|*.xlsx|All Files(*.*)|*.*",
                FilterIndex = 1
            };

            if (sfd.ShowDialog() != true)
                return null;
            return sfd.FileName;
        }

        private static void SaveFile(HSSFWorkbook book, string filepath)
        {
            try
            {
                //写入到客户端
                using (MemoryStream ms = new MemoryStream())
                {
                    book.Write(ms);
                    using (FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write))
                    {
                        byte[] data = ms.ToArray();
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                    }
                    book = null;
                }
            }
            catch (Exception e)
            {
                MsgBox.WarningShow("", e.Message);
            }
        }
    }
}

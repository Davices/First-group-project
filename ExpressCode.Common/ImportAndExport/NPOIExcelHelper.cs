using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

/// <summary>
/// 注意修改命名空间
/// </summary>
namespace ExpressCode.Common
{
    public static class NPOIExcelHelper
    {
        #region Excel导入
        /// <summary>
        /// Excel导入,返回Datable
        /// </summary>
        /// <param name="file">要导入的Excel文件(包含路径,文件名与扩展名)</param>
        /// <returns>返回Datable</returns>
        public static DataTable ExcelToTable(string file)
        {
            try
            {
                //定义DT
                DataTable dt = new DataTable();
                //工作表
                IWorkbook workbook = null;
                //获取文件
                string fileExt = Path.GetExtension(file).ToLower();
                //读取文件
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                    if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); }
                    else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); }
                    else { return null; }
                    //if (workbook == null) { return null; }
                    ISheet sheet = workbook.GetSheetAt(0); //获取第1页

                    //表头 第1行位表头  
                    IRow header = sheet.GetRow(sheet.FirstRowNum);
                    List<int> columns = new List<int>();
                    //讲表头填充到DataTable
                    for (int i = 0; i < header.LastCellNum; i++)
                    {
                        object obj = GetValueType(header.GetCell(i));
                        if (obj == null || obj.ToString() == string.Empty)
                        {
                            dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                        }
                        else
                            dt.Columns.Add(new DataColumn(obj.ToString()));
                        columns.Add(i);
                    }

                    for (int k = 0; k < workbook.NumberOfSheets; k++) //NumberOfSheets是xls文件中总共的表数
                    {
                        sheet = workbook.GetSheetAt(k); //获取第1页

                        ////表头 第1行位表头  
                        //IRow header = sheet.GetRow(sheet.FirstRowNum);
                        //List<int> columns = new List<int>();
                        ////讲表头填充到DataTable
                        //for (int i = 0; i < header.LastCellNum; i++)
                        //{
                        //    object obj = GetValueType(header.GetCell(i));
                        //    if (obj == null || obj.ToString() == string.Empty)
                        //    {
                        //        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                        //    }
                        //    else
                        //        dt.Columns.Add(new DataColumn(obj.ToString()));
                        //    columns.Add(i);
                        //}
                        //数据填充到DataTable  
                        for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                        {
                            DataRow dr = dt.NewRow();
                            bool hasValue = false;
                            foreach (int j in columns)
                            {
                                dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                                if (dr[j] != null && dr[j].ToString() != string.Empty)
                                {
                                    hasValue = true;
                                }
                            }
                            if (hasValue)
                            {
                                dt.Rows.Add(dr);
                            }
                        }
                    }

                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Datable导出
        /// <summary>
        /// Datable导出,保存为单页签Excel
        /// </summary>
        /// <param name="dt">要导出的Datable</param>
        /// <param name="file">Excel文件(包括路径,文件名与扩展名)</param>
        public static void TableToExcel(DataTable dt, string file)
        {
            try
            {
                IWorkbook workbook;
                string fileExt = Path.GetExtension(file).ToLower();
                if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(); } else { workbook = null; }
                if (workbook == null) { return; }
                ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);

                //表头  
                IRow row = sheet.CreateRow(0);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(dt.Columns[i].ColumnName);
                }

                //数据  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row1 = sheet.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ICell cell = row1.CreateCell(j);
                        cell.SetCellValue(dt.Rows[i][j].ToString());
                    }
                }

                //转为字节数组  
                MemoryStream stream = new MemoryStream();
                workbook.Write(stream);
                var buf = stream.ToArray();

                //保存为Excel文件  
                using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(buf, 0, buf.Length);
                    fs.Flush();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Datable导出
        /// <summary>
        /// Datable导出,保存为单页签Excel,MVC专用 
        /// </summary>
        /// <param name="dt">要导出的Datable</param>
        /// <param name="file">Excel文件(包括路径,文件名与扩展名) application/vnd.ms-excel</param>
        public static MemoryStream TableToExcelForMVC(DataTable dt, string file = "demo.xls")
        {
            try
            {
                IWorkbook workbook;
                string fileExt = Path.GetExtension(file).ToLower();
                if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(); } else { workbook = null; }
                if (workbook == null) { return null; }
                ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);

                //表头  
                IRow row = sheet.CreateRow(0);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(dt.Columns[i].ColumnName);
                }

                //数据  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row1 = sheet.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ICell cell = row1.CreateCell(j);
                        cell.SetCellValue(dt.Rows[i][j].ToString());
                    }
                }

                //转为字节数组  
                NpoiMemoryStream stream = new NpoiMemoryStream();
                stream.AllowClose = false;
                workbook.Write(stream);
                stream.Seek(0, SeekOrigin.Begin);
                stream.AllowClose = true;
                return stream;
                //var buf = stream.ToArray();

                ////保存为Excel文件  
                //using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
                //{
                //    fs.Write(buf, 0, buf.Length);
                //    fs.Flush();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion




        //新建类 重写Npoi流方法
        public class NpoiMemoryStream : MemoryStream
        {
            public NpoiMemoryStream()
            {
                AllowClose = true;
            }

            public bool AllowClose { get; set; }

            public override void Close()
            {
                if (AllowClose)
                    base.Close();
            }
        }

        /// <summary>
        /// Datable导出,保存为1个或多个页签的Excel
        /// </summary>
        /// <param name="dt">要导出的Datable</param>
        /// <param name="file">Excel文件(包括路径,文件名与扩展名)</param>
        /// <param name="sheetCount">页签数</param>
        public static void TableToExcelForSheets(DataTable dt, string file, int sheetCount = 1)
        {
            try
            {
                IWorkbook workbook;
                string fileExt = Path.GetExtension(file).ToLower();
                if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(); } else { workbook = null; }
                if (workbook == null) { return; }
                //页签数判断
                if (sheetCount == 1)
                {
                    ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);

                    //表头  
                    IRow row = sheet.CreateRow(0);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        ICell cell = row.CreateCell(i);
                        cell.SetCellValue(dt.Columns[i].ColumnName);
                    }

                    //数据  
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row1 = sheet.CreateRow(i + 1);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            ICell cell = row1.CreateCell(j);
                            cell.SetCellValue(dt.Rows[i][j].ToString());
                        }
                    }
                }
                else
                {
                    for (int k = 1; k <= sheetCount; k++)
                    {
                        ISheet sheet = workbook.CreateSheet("Sheet" + k);

                        //表头  
                        IRow row = sheet.CreateRow(0);
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            ICell cell = row.CreateCell(i);
                            cell.SetCellValue(dt.Columns[i].ColumnName);
                        }

                        //数据  
                        var rows = (dt.Rows.Count / sheetCount);
                        var n = 0;
                        for (int i = rows * (k - 1); i < rows * k; i++)
                        {
                            IRow row1 = sheet.CreateRow(n + 1);
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                ICell cell = row1.CreateCell(j);
                                cell.SetCellValue(dt.Rows[i][j].ToString());
                            }
                            n++;
                        }
                    }
                }

                //转为字节数组  
                MemoryStream stream = new MemoryStream();
                workbook.Write(stream);
                var buf = stream.ToArray();

                //保存为Excel文件  
                using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(buf, 0, buf.Length);
                    fs.Flush();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Datable导出,保存为1个或多个页签的Excel
        /// </summary>
        /// <param name="dt">要导出的Datable</param>
        /// <param name="file">Excel文件(包括路径,文件名与扩展名)</param>
        /// <param name="rows">每页的记录数</param>
        public static void TableToExcelForRows(DataTable dt, string file, int rows)
        {
            try
            {
                IWorkbook workbook;
                string fileExt = Path.GetExtension(file).ToLower();
                if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(); } else { workbook = null; }
                if (workbook == null) { return; }
                var sheetCount = Math.Ceiling(((double)dt.Rows.Count / (double)rows));
                //页签数判断
                if (sheetCount == 1)
                {
                    ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);

                    //表头  
                    IRow row = sheet.CreateRow(0);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        ICell cell = row.CreateCell(i);
                        cell.SetCellValue(dt.Columns[i].ColumnName);
                    }

                    //数据  
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row1 = sheet.CreateRow(i + 1);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            ICell cell = row1.CreateCell(j);
                            cell.SetCellValue(dt.Rows[i][j].ToString());
                        }
                    }
                }
                else if (sheetCount > 1)
                {
                    int rowcount;
                    for (int k = 1; k <= sheetCount; k++)
                    {
                        ISheet sheet = workbook.CreateSheet("Sheet" + k);

                        //表头  
                        IRow row = sheet.CreateRow(0);
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            ICell cell = row.CreateCell(i);
                            cell.SetCellValue(dt.Columns[i].ColumnName);
                        }

                        //数据  
                        //var rows = (dt.Rows.Count / sheetCount);
                        var n = 0;
                        rowcount = rows * k;
                        //如果越界,取最大值
                        if (rowcount > dt.Rows.Count)
                        {
                            rowcount = dt.Rows.Count;
                        }
                        for (int i = rows * (k - 1); i < rowcount; i++)
                        {
                            IRow row1 = sheet.CreateRow(n + 1);
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                ICell cell = row1.CreateCell(j);
                                cell.SetCellValue(dt.Rows[i][j].ToString());
                            }
                            n++;
                        }
                    }
                }

                //转为字节数组  
                MemoryStream stream = new MemoryStream();
                workbook.Write(stream);
                var buf = stream.ToArray();

                //保存为Excel文件  
                using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(buf, 0, buf.Length);
                    fs.Flush();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //获取单元格值
        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:  
                    return null;
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:  
                    return cell.NumericCellValue;
                case CellType.String: //STRING:  
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:  
                default:
                    return "=" + cell.CellFormula;
            }
        }

    }

}

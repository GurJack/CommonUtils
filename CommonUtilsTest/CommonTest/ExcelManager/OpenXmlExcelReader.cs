//using System;
//using System.Collections.Generic;
//using System.Linq;
//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Spreadsheet;

//namespace ExcelProcess
//{
//    public class OpenXmlExcelReader : IDisposable
//    {
       

        

//        private readonly string _fileName;
//        private SpreadsheetDocument _document;
//        private WorkbookPart WorkbookPart => _document.WorkbookPart;
//        private Sheet Sheet => WorkbookPart.Workbook.Descendants<Sheet>().FirstOrDefault();
//        private WorksheetPart WorksheetPart => (WorksheetPart)WorkbookPart.GetPartById(Sheet.Id);
//        private Stylesheet StyleSheet => WorkbookPart.WorkbookStylesPart.Stylesheet;
//        private SharedStringTable SharedStringTable => WorkbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault()?.SharedStringTable;

//        private IEnumerable<Row> Rows => WorksheetPart.Worksheet.GetFirstChild<SheetData>().Elements<Row>().ToList();
//        private IEnumerable<Column> Columns => WorksheetPart.Worksheet.Descendants<Column>().ToList();

//        public OpenXmlExcelReader(string fileName)
//        {
//            _fileName = fileName;
//            Open();
//        }

//        private void Open() => _document = SpreadsheetDocument.Open(_fileName, false);

//        public object[,] ReadAll()
//        {
//            var rowIndex = 0;
//            var n = Rows.Count();
//            var m = Rows.FirstOrDefault()?.Elements<Cell>().Count() ?? 0;
//            var result = Array.CreateInstance(typeof(object), new[] { n, m }, new[] { 1, 1 });
//            foreach (var row in Rows)
//            {
//                var obj = row.Elements<Cell>().Select(TryParse).ToArray();
//                rowIndex++;
//                for (var i = 1; i <= m; i++)
//                    result.SetValue(obj[i - 1], rowIndex, i);
//            }
//            return result as object[,];
//        }


//        private (
//            CellFormat CellFormat,
//            NumberingFormat NumberFormat,
//            EnumValue<CellValues> CellValue) CellFormatsInfo(Cell cell)
//        {
//            if (cell == null)
//                throw new ArgumentNullException(nameof(cell));
//            EnumValue<CellValues> cellValue = cell.DataType;
//            CellFormat cellFormat = null;
//            NumberingFormat numberFormat = null;
//            var csIndex = cell.StyleIndex;
//            if (csIndex != null)
//            {
//                cellFormat = StyleSheet
//                    .Descendants<CellFormats>()
//                    .FirstOrDefault()
//                    ?.Descendants<CellFormat>()
//                    .ToList()[(int)csIndex.Value];
//            }

//            if (cellFormat?.NumberFormatId != null)
//            {
//                var nIndex = cellFormat.NumberFormatId;
//                numberFormat = StyleSheet
//                    .Descendants<NumberingFormats>()
//                    .FirstOrDefault()
//                    ?.Descendants<NumberingFormat>()
//                    .FirstOrDefault(p => p.NumberFormatId.Value == nIndex.Value);
//            }
//            return (cellFormat, numberFormat, cellValue);
//        }


//        public object TryParse(Cell cell)
//        {
//            var formatsInfo = CellFormatsInfo(cell);
//            var defaultValue = cell.InnerText;

//            switch (formatsInfo)
//            {
//                case ValueTuple<CellFormat, NumberingFormat, EnumValue<CellValues>> f
//                when f.Item3 != null && f.Item3 == CellValues.SharedString:
//                    return SharedStringTable?.ElementAt(int.Parse(defaultValue)).InnerText;
//                case ValueTuple<CellFormat, NumberingFormat, EnumValue<CellValues>> f
//                when f.Item3 != null && f.Item3 == CellValues.Boolean:
//                    return defaultValue != "0";
//                case ValueTuple<CellFormat, NumberingFormat, EnumValue<CellValues>> f
//                when f.Item3 != null && f.Item3 == CellValues.Date:
//                    return DateTime.FromOADate(double.Parse(defaultValue));
//                case ValueTuple<CellFormat, NumberingFormat, EnumValue<CellValues>> f
//                when f.Item1 != null && f.Item1.NumberFormatId == 0:
//                    return defaultValue;
//                case ValueTuple<CellFormat, NumberingFormat, EnumValue<CellValues>> f
//                when f.Item1 != null && f.Item1.NumberFormatId > 0 && f.Item1.NumberFormatId < 13:
//                    return GetDouble(defaultValue);
//                case ValueTuple<CellFormat, NumberingFormat, EnumValue<CellValues>> f
//                when f.Item1 != null && f.Item1.NumberFormatId > 13 && f.Item1.NumberFormatId < 23:
//                    return GetDateTime(defaultValue);
//                case ValueTuple<CellFormat, NumberingFormat, EnumValue<CellValues>> f
//                when defaultValue.All(char.IsDigit):
//                    return GetDouble(defaultValue);
//                default:
//                    return defaultValue;
//            }
//        }

//        public void Dispose() => _document.Close();

//        private DateTime GetDateTime(string value) => DateTime.FromOADate(double.Parse(value));

//        private int GetInt(string value) => int.Parse(value);

//        private double GetDouble(string value) => double.Parse(value);

//        /*
//        * BUILT-IN FORMATS
//        0 General
//        1 0
//        2 0.00
//        3 #,##0
//        4 #,##0.00
//        9 0%
//        10 0.00%
//        11 0.00E+00
//        12 # ?/?
//        13 # ??/??
//        14 mm-dd-yy
//        15 d-mmm-yy
//        16 d-mmm
//        17 mmm-yy
//        18 h:mm AM/PM
//        19 h:mm:ss AM/PM
//        20 h:mm
//        21 h:mm:ss
//        22 m/d/yy h:mm
//        37 #,##0 ;(#,##0)
//        38 #,##0 ;[Red](#,##0)
//        39 #,##0.00;(#,##0.00)
//        40 #,##0.00;[Red](#,##0.00)
//        45 mm:ss
//        46 [h]:mm:ss
//        47 mmss.0
//        48 ##0.0E+0
//        49 @
//        */
//    }
//}
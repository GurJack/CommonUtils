using System;
using System.Linq;

namespace CommonUtils.ExcelManager
{
    public class Excel : IDisposable
    {
		private readonly string _FileName;
		private dynamic _WorkBook;
		private dynamic _Excel;

	    public static void ReadAll(string fileName,out object[,] listFormula,out object[,] listValues)
        {
            //comment

            var xlAppType = Type.GetTypeFromProgID("Excel.Application");
            dynamic xl = Activator.CreateInstance(xlAppType);
            xl.DisplayAlerts = false;
            xl.Workbooks.Open(fileName, Type.Missing, true);
            var workbook = xl.Workbooks[1];
            var sheet = workbook.WorkSheets[1];

			sheet.Select();
			var range = sheet.Range(sheet.Range("A1"), xl.ActiveCell.SpecialCells(11));
			listFormula = range.Formula;
			listValues = range.Value;
            xl.Workbooks.Close();
            xl.Quit();
        }
		public static object[,] ReadAll(string fileName)=> ExcelLib.ReadToEnd(fileName);

        public static object[,] ComReadAll(string fileName)
        {
            var xlAppType = Type.GetTypeFromProgID("Excel.Application");
            dynamic xl = Activator.CreateInstance(xlAppType);
            xl.DisplayAlerts = false;
            xl.Workbooks.Open(fileName, Type.Missing, true);
            var workbook = xl.Workbooks[1];
            var sheet = workbook.WorkSheets[1];

            sheet.Select();
            var range = sheet.Range(sheet.Range("A1"), xl.ActiveCell.SpecialCells(11));
            var list = (range.Value is string) ? new[,] { { (object)range.Value } } : range.Value;
            xl.Workbooks.Close();
            xl.Quit();
            return list;
        }


        public Excel(string FileName)
		{
			_FileName = FileName;
			var xlAppType = Type.GetTypeFromProgID("Excel.Application");
            _Excel = Activator.CreateInstance(xlAppType);
			_WorkBook = _Excel.Workbooks.Add();
		}

        public Excel(string fileName, int sheetsCount)
        {
            _FileName = fileName;
            var xlAppType = Type.GetTypeFromProgID("Excel.Application");
            _Excel = Activator.CreateInstance(xlAppType);
            _Excel.SheetsInNewWorkbook = sheetsCount;
            _WorkBook = _Excel.Workbooks.Add();

            //Excel.Application appC = new Excel.Application();
            //appC.SheetsInNewWorkbook = 1;
            //appC.Visible = true;
            //Excel.Workbook bookC = appC.Workbooks.Add();
            //Excel.Worksheet sheetC = appC.Sheets.get_Item(1);
            //sheetC.Name = "name-of-sheet";
        }
        public void Dispose()
    	{
    		if (string.IsNullOrWhiteSpace(_FileName))
    			_Excel.Visible = true;
    		else
    		{
    			_Excel.DisplayAlerts = false;
				_WorkBook.SaveCopyAs(_FileName);
    			_WorkBook.Close();
		        _Excel.Quit();

		    }
    	}

        [Obsolete("Используй WriteArrayStr. Лишние r нам не нужны.", true)]
        public void WriteArrrayStr(int sheetNo, object[,] list)
        {
            dynamic range, sheet;
            sheet = _WorkBook.WorkSheets.Item(sheetNo);
            range = sheet.Range("A1:" + ColNumToAlpha(list.GetLength(1)) + list.GetLength(0).ToString());
            range.Value = list;
            range = sheet.Range("A1:" + ColNumToAlpha(list.GetLength(1)) + list.GetLength(0).ToString());
            range.Columns.AutoFit();
            range.HorizontalAlignment = -4108;

            //InsideBorders(sheet.Range("A2:" + ColNumToAlpha(list.GetLength(1)) + list.GetLength(0).ToString()));
            //OutsideBorders(sheet.Range("A2:" + ColNumToAlpha(list.GetLength(1)) + list.GetLength(0).ToString()));
            //OutsideBorders(sheet.Range("A2:" + ColNumToAlpha(list.GetLength(1)) + 2.ToString()));
        }

        public void WriteArrayStr(int sheetNo, object[,] list)
        {
            dynamic range, sheet;
            sheet = _WorkBook.WorkSheets.Item(sheetNo);
            range = sheet.Range("A1:" + ColNumToAlpha(list.GetLength(1)) + list.GetLength(0).ToString());
            range.Value = list;
            range = sheet.Range("A1:" + ColNumToAlpha(list.GetLength(1)) + list.GetLength(0).ToString());
            range.Columns.AutoFit();
            range.HorizontalAlignment = -4108;

            //InsideBorders(sheet.Range("A2:" + ColNumToAlpha(list.GetLength(1)) + list.GetLength(0).ToString()));
            //OutsideBorders(sheet.Range("A2:" + ColNumToAlpha(list.GetLength(1)) + list.GetLength(0).ToString()));
            //OutsideBorders(sheet.Range("A2:" + ColNumToAlpha(list.GetLength(1)) + 2.ToString()));
        }
        public void WriteArrray(int sheetNo , object[,] list, int ThickCol , int Width, int precision = 1)
		{
			dynamic sheet = _WorkBook.WorkSheets.Item(sheetNo);
			dynamic range1 = sheet.Range(ColNumToAlpha(ThickCol + 1) + "3:" + ColNumToAlpha(list.GetLength(1)) + list.GetLength(0).ToString());
			dynamic range = sheet.Range("A1:" + ColNumToAlpha(list.GetLength(1)) + list.GetLength(0).ToString());
			range.HorizontalAlignment = -4108;
		    if (precision > 0)
                range1.NumberFormat = "0" + _Excel.DecimalSeparator + string.Concat(Enumerable.Repeat("0",precision));
			range.Value = list;
			range.Columns.AutoFit();
			range1 =
				sheet.Range(ColNumToAlpha(ThickCol + 1) + "3:" + ColNumToAlpha(list.GetLength(1) - 3) + list.GetLength(0).ToString());
			if (Width > 0)
				range1.ColumnWidth = Width;

			InsideBorders(sheet.Range("A2:" + ColNumToAlpha(list.GetLength(1)) + list.GetLength(0).ToString()));
			OutsideBorders(sheet.Range("A2:" + ColNumToAlpha(list.GetLength(1)) + list.GetLength(0).ToString()));
			OutsideBorders(sheet.Range("A2:" + ColNumToAlpha(list.GetLength(1)) + 2.ToString()));
			for (var i = 1; i <= ThickCol; i++)
				OutsideBorders(sheet.Range(ColNumToAlpha(i) + "2:" + "A" + list.GetLength(0).ToString()));
		}
    	

        public void WriteArray1(int sheetNo, object[,] list, int ThickCol, int Width, int precision = 1)
        {
            dynamic sheet = _WorkBook.WorkSheets.Item(sheetNo);
            dynamic range1 = sheet.Range(ColNumToAlpha(ThickCol + 1) + "3:" + ColNumToAlpha(list.GetLength(1)) + list.GetLength(0).ToString());
            dynamic range = sheet.Range("A1:" + ColNumToAlpha(list.GetLength(1)) + list.GetLength(0).ToString());
            range.HorizontalAlignment = -4108;
            if (precision > 0)
                range1.NumberFormat = "0" + _Excel.DecimalSeparator + string.Concat(Enumerable.Repeat("0", precision));
            range.Value = list;
            range.Columns.AutoFit();
            range1 =
                sheet.Range(ColNumToAlpha(ThickCol + 1) + "3:" + ColNumToAlpha(list.GetLength(1) - 1) + list.GetLength(0).ToString());
            if (Width > 0)
                range1.ColumnWidth = Width;

            InsideBorders(sheet.Range("A2:" + ColNumToAlpha(list.GetLength(1)) + list.GetLength(0).ToString()));
            OutsideBorders(sheet.Range("A2:" + ColNumToAlpha(list.GetLength(1)) + list.GetLength(0).ToString()));
            OutsideBorders(sheet.Range("A2:" + ColNumToAlpha(list.GetLength(1)) + 2.ToString()));
            for (var i = 1; i <= ThickCol; i++)
                OutsideBorders(sheet.Range(ColNumToAlpha(i) + "2:" + "A" + list.GetLength(0).ToString()));
        }

    	private void InsideBorders(dynamic range)
    	{
    		range.Borders(7).Weight = 2;
    		range.Borders(8).Weight = 2;
    		range.Borders(9).Weight = 2;
    		range.Borders(10).Weight = 2;
    		range.Borders(11).Weight = 2;
    		range.Borders(12).Weight = 2;
    	}
		private void OutsideBorders(dynamic range)
		{
		    range.Borders(7).Weight = 2;//-4138;
            range.Borders(8).Weight = 2;//-4138;
            range.Borders(9).Weight = 2;//-4138;
            range.Borders(10).Weight = 2;//-4138;
		}
		public void WriteCell(int sheetNo, int Row, int Col, string Value, int Left, int Top, int Right, int Bottom)
		{
			dynamic range, sheet;
			sheet = _WorkBook.WorkSheets.Item(sheetNo);
			range = sheet.Range(ColNumToAlpha(Col) + Row.ToString());
			try
			{
				range.Value = Convert.ToDouble(Value);
			}
			catch (Exception)
			{
				range.Value = Value;
			}

			if (Left == 2)
				range.Borders(7).Weight = -4138;
			else if (Left == 1)
				range.Borders(7).Weight = 2;

			if (Top == 2)
				range.Borders(8).Weight = -4138;
			else if (Top == 1)
				range.Borders(8).Weight = 2;

			if (Right == 2)
				range.Borders(10).Weight = -4138;
			else if (Right == 1)
				range.Borders(10).Weight = 2;

			if (Bottom == 2)
				range.Borders(9).Weight = -4138;
			else if (Bottom == 1)
				range.Borders(9).Weight = 2;
		}
        public void WriteCell(int sheetNo, int Row, int Col, string Value, string Format, int hAligntent = -4138)
    	{
    		dynamic range, sheet;
    		sheet = _WorkBook.WorkSheets.Item(sheetNo);
    		range = sheet.Range(ColNumToAlpha(Col) + Row.ToString());
            range.HorizontalAlignment = -4131;
    		range.Value = Value;
    		if (Format != null)
    			range.NumberFormat = Format;
    	}
    	private string ColNumToAlpha(int ColumnNumber)
		{
			var ColNum = ColumnNumber;
			var ColLetters = "";
			do
			{
				ColLetters = (char) (((ColNum - 1)%26) + 65) + ColLetters;
				ColNum = ColNum /26;
			} while(ColNum > 0);
		        return ColLetters;
		}
		public void CenterAlignRow(int sheetNo, int RowBegin, int RowEnd)
		{
			var sheet = _WorkBook.WorkSheets.Item(sheetNo);
			sheet.Rows(RowBegin.ToString() + ":" + RowEnd.ToString()).HorizontalAlignment = -4108;
		}
		public void AutoFit(int sheetNo, int ColBegin, int ColEnd)
		{
			var sheet = _WorkBook.WorkSheets.Item(sheetNo);
			sheet.Columns(ColNumToAlpha(ColBegin) + ":" + ColNumToAlpha(ColEnd)).Columns.AutoFit();
			sheet.Cells.Columns.AutoFit();
		}
		public void ColumnWidth(int sheetNo, int ColBegin, int ColEnd, double Width)
		{
			var sheet = _WorkBook.WorkSheets.Item(sheetNo);
			sheet.Columns(ColNumToAlpha(ColBegin) + ":" + ColNumToAlpha(ColEnd)).ColumnWidth = Width;
		}
		public void SetName(int sheetNo, string name)
		{
		    var countSheets = _WorkBook.WorkSheets.Count;
		    if (countSheets < sheetNo)
		        _WorkBook.WorkSheets.Add(After: _WorkBook.WorkSheets[countSheets]);

			var sheet = _WorkBook.WorkSheets.Item(sheetNo);
            sheet.Name = name;
		}

        public int AddWorkSheet(string nameWorkSheet)
        {
            var count = _WorkBook.WorkSheets.Count;
            _WorkBook.WorkSheets.Add(After: _WorkBook.WorkSheets[count]);
            var newSheet = _WorkBook.WorkSheets.Item(count + 1);
            newSheet.Name = nameWorkSheet;
            return count + 1;
        }

        public void AlignColumn(int sheetNo, int columnNo, HorizontalAlignEnum align)
        {
            var sheet = _WorkBook.WorkSheets.Item(sheetNo);
            sheet.Range(ColNumToAlpha(columnNo) + ":" + ColNumToAlpha(columnNo)).HorizontalAlignment = (int)align;
        }
	    public void AddChart()
	    {
			_WorkBook.ActiveSheet.Shapes.AddChart2(240, 75).Select();
			_WorkBook.ActiveChart.Location(1);
		}
	    public void SetChart(int SeriesCount)
	    {
		    var chart = _WorkBook.ActiveChart;
		    chart.ChartArea.Select();
		    while (chart.FullSeriesCollection.Count > 0)
			    chart.FullSeriesCollection(1).Delete();
		    for (var i = 0; i < SeriesCount; i++)
		    {
			    chart.SeriesCollection.NewSeries();
				chart.FullSeriesCollection(i + 1).Name = string.Format("=Лист1!${0}$1", ColNumToAlpha(2*i+1));
				chart.FullSeriesCollection(i + 1).XValues = string.Format("=Лист1!${0}:${0}", ColNumToAlpha(2 * i + 1));
				chart.FullSeriesCollection(i + 1).Values = string.Format("=Лист1!${0}:${0}", ColNumToAlpha(2 * i + 2));
		    }
	    }

        public void CellMerging(int sheetNo, int rowStart, int columnStart, int rowEnd, int columnEnd)
        {
            dynamic range, sheet;
            sheet = _WorkBook.WorkSheets.Item(sheetNo);
            var rangeStr = $"{ColNumToAlpha(columnStart)}{rowStart}:{ColNumToAlpha(columnEnd)}{rowEnd}";
            range = sheet.Range(rangeStr);
            range.Merge();
        }

        [Obsolete("Используйте FormatRow", true)]
        public void RowFormater(int sheetNo, int rowStart, int columnStart, int rowEnd, int columnEnd, FrontStyleEnum frontStyle, int frontSize, HorizontalAlignEnum hAlign, HorizontalAlignEnum vAlign)
        {
            dynamic range, sheet;
            sheet = _WorkBook.WorkSheets.Item(sheetNo);
            var rangeStr = $"{ColNumToAlpha(columnStart)}{rowStart}:{ColNumToAlpha(columnEnd)}{rowEnd}";
            range = sheet.Range(rangeStr);
            if (frontStyle == FrontStyleEnum.Regular)
            {
                range.Font.Bold = false;
                range.Font.Italic = false;
            }
            if (frontStyle == FrontStyleEnum.Bold)
            {
                range.Font.Bold = true;
                range.Font.Italic = false;
            }
            if (frontStyle == FrontStyleEnum.Italic)
            {
                range.Font.Bold = false;
                range.Font.Italic = true;
            }
            //range.Font.FontStyle = frontStyle;
            range.Font.Size = frontSize;
            range.HorizontalAlignment = hAlign;
            range.VerticalAlignment = vAlign;
        }

        public void FormatRow(int sheetNo, int rowStart, int columnStart, int rowEnd, int columnEnd, FrontStyleEnum frontStyle, int frontSize, HorizontalAlignEnum hAlign, HorizontalAlignEnum vAlign)
        {
            dynamic range, sheet;
            sheet = _WorkBook.WorkSheets.Item(sheetNo);
            var rangeStr = $"{ColNumToAlpha(columnStart)}{rowStart}:{ColNumToAlpha(columnEnd)}{rowEnd}";
            range = sheet.Range(rangeStr);
            if (frontStyle == FrontStyleEnum.Regular)
            {
                range.Font.Bold = false;
                range.Font.Italic = false;
            }
            if (frontStyle == FrontStyleEnum.Bold)
            {
                range.Font.Bold = true;
                range.Font.Italic = false;
            }
            if (frontStyle == FrontStyleEnum.Italic)
            {
                range.Font.Bold = false;
                range.Font.Italic = true;
            }
            range.Font.Size = frontSize;
            range.HorizontalAlignment = hAlign;
            range.VerticalAlignment = vAlign;
        }
        public void FormatRow(int sheetNo, int rowStart, int columnStart, int rowEnd, int columnEnd, FrontStyleEnum frontStyle, int frontSize, HorizontalAlignEnum hAlign)
        {
            dynamic range, sheet;
            sheet = _WorkBook.WorkSheets.Item(sheetNo);
            var rangeStr = $"{ColNumToAlpha(columnStart)}{rowStart}:{ColNumToAlpha(columnEnd)}{rowEnd}";
            range = sheet.Range(rangeStr);
            if (frontStyle == FrontStyleEnum.Regular)
            {
                range.Font.Bold = false;
                range.Font.Italic = false;
            }
            if (frontStyle == FrontStyleEnum.Bold)
            {
                range.Font.Bold = true;
                range.Font.Italic = false;
            }
            if (frontStyle == FrontStyleEnum.Italic)
            {
                range.Font.Bold = false;
                range.Font.Italic = true;
            }
            range.Font.Size = frontSize;
            range.HorizontalAlignment = hAlign;
        }

        public void SetSheetName(int sheetNo, string name)
        {
            var sheet = _WorkBook.WorkSheets.Item(sheetNo);
            sheet.Name = name;
        }

        public void SetAutoFilter(int sheetNo, int rowNo, int colStart, int colEnd)
        {
            dynamic range, sheet;
            sheet = _WorkBook.WorkSheets.Item(sheetNo);
            var rangeStr = $"{ColNumToAlpha(colStart)}{rowNo}:{ColNumToAlpha(colEnd)}{rowNo}";
            range = sheet.Range(rangeStr);
            range.AutoFilter();
        }

        public void ColumnNumberFormat(int sheetNo, int rowStart, int rowEnd, int Col, string Format)
        {
            dynamic range, sheet;
            sheet = _WorkBook.WorkSheets.Item(sheetNo);
            var rangeStr = $"{ColNumToAlpha(Col)}{rowStart}:{ColNumToAlpha(Col)}{rowEnd}";
            range = sheet.Range(rangeStr);
                range.NumberFormat = Format;
        }
    }
}

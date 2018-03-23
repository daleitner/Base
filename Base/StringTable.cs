using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base
{
	/*
     * This class is used for generating an ascii formatted string table.
     * The dataformat of the table is a List consisting of arrays.
     * Each array represents one row.
     * */
	public class StringTable
	{
		#region ascii chars
		private const char line_horizontal = '─';
		private const char line_vertical = '│';

		private const char padding = ' ';
		private const string column_header_padding_pre = " [";
		private const string column_header_padding_post = "] ";
		private const string section_header_padding = "Δ ";
		private const string sub_section_header_padding = "➝ ";

		private const char connection_top_left = '┌';
		private const char connection_top = '┬';
		private const char connection_top_right = '┐';

		private const char connection_left = '├';
		private const char connection = '┼';
		private const char connection_right = '┤';

		private const char connection_bottom_left = '└';
		private const char connection_bottom = '┴';
		private const char connection_bottom_right = '┘';
		#endregion

		private List<object[]> table;

		public StringTable()
		{
			this.Table = new List<object[]>();
		}

		public StringTable(List<object[]> data)
		{
			this.Table = data;
		}

		public List<object[]> Table
		{
			get
			{
				return this.table;
			}
			set
			{
				this.table = value;
			}
		}

		private object[] AddPadding(object[] row, bool isHeader = false, bool isSectionHeader = false)
		{
			for (int i = 0; i < row.Length; i++)
			{
				string content = GetString(row[i]);
				if (!String.IsNullOrEmpty(content))
				{
					if (isSectionHeader && isHeader)
						row[i] = String.Format("{0}{1}{2}", section_header_padding, content, padding);
					else if (isSectionHeader)
						row[i] = String.Format("{0}{1}{2}", sub_section_header_padding, content, padding);
					else if (isHeader)
						row[i] = String.Format("{0}{1}{2}", column_header_padding_pre, content, column_header_padding_post);
					else
						row[i] = String.Format("{0}{1}{2}", padding, content, padding);
				}
				else
					row[i] = String.Empty;

			}
			return row;
		}

		private string GetString(object obj)
		{
			if (obj == null)
				return String.Empty;

			return obj.ToString();
		}

		public void AddRow(object[] row)
		{
			row = AddPadding(row);
			this.Table.Add(row);
		}

		public void AddTitleRow(object[] row)
		{
			row = AddPadding(row, true);
			this.Table.Add(row);
		}

		public string GetFormattedString(int inherit = 0)
		{
			string tableString = FormatStringTable();
			string newTableString = tableString;

			if (inherit > 0)
			{
				newTableString = String.Empty;

				string inheritString = "";
				for (int i = 0; i < inherit; i++)
					inheritString += "\t";

				foreach (string line in tableString.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
					newTableString += inheritString + line + Environment.NewLine;
			}

			newTableString = newTableString.TrimEnd('\r', '\n');

			return newTableString;
		}

		private int[] GetMaxCellWidths()
		{
			int maximumCells = 0;
			foreach (Array row in this.Table)
			{
				if (row.Length > maximumCells)
					maximumCells = row.Length;
			}

			int[] maximumCellWidths = new int[maximumCells];
			for (int i = 0; i < maximumCellWidths.Length; i++)
				maximumCellWidths[i] = 0;

			foreach (Array row in this.Table)
			{
				for (int i = 0; i < row.Length; i++)
				{
					if (row.GetValue(i).ToString().Length > maximumCellWidths[i])
						maximumCellWidths[i] = row.GetValue(i).ToString().Length;
				}
			}

			return maximumCellWidths;
		}

		private string FormatStringTable()
		{
			StringBuilder formattedTable = new StringBuilder();
			Array nextRow = this.Table.FirstOrDefault();
			Array previousRow = this.Table.FirstOrDefault();

			if (this.Table == null || nextRow == null)
				return String.Empty;

			int[] maximumCellWidths = GetMaxCellWidths();
			for (int i = 0; i < nextRow.Length; i++)
			{
				if (i == 0 && i == nextRow.Length - 1)
					formattedTable.AppendLine(String.Format("{0}{1}{2}", connection_top_left, String.Empty.PadRight(maximumCellWidths[i], line_horizontal), connection_top_right));
				else if (i == 0)
					formattedTable.Append(String.Format("{0}{1}", connection_top_left, String.Empty.PadRight(maximumCellWidths[i], line_horizontal)));
				else if (i == nextRow.Length - 1)
					formattedTable.AppendLine(String.Format("{0}{1}{2}", connection_top, String.Empty.PadRight(maximumCellWidths[i], line_horizontal), connection_top_right));
				else
					formattedTable.Append(String.Format("{0}{1}", connection_top, String.Empty.PadRight(maximumCellWidths[i], line_horizontal)));
			}

			int rowIndex = 0;
			int lastRowIndex = this.Table.Count - 1;
			foreach (Array row in this.Table)
			{
				int cellIndex = 0;
				int lastCellIndex = row.Length - 1;
				foreach (object thisCell in row)
				{
					string thisValue = thisCell.ToString().PadRight(maximumCellWidths[cellIndex], padding);

					if (cellIndex == lastCellIndex)
						formattedTable.AppendLine(String.Format("{0}{1}{2}", line_vertical, thisValue, line_vertical));
					else
						formattedTable.Append(String.Format("{0}{1}", line_vertical, thisValue));

					cellIndex++;
				}

				previousRow = row;

				if (rowIndex != lastRowIndex)
				{
					nextRow = this.Table[rowIndex + 1];

					int maximumCells = Math.Max(previousRow.Length, nextRow.Length);
					for (int i = 0; i < maximumCells; i++)
					{
						if (i == 0 && i == maximumCells - 1)
							formattedTable.AppendLine(String.Format("{0}{1}{2}", connection_left, String.Empty.PadRight(maximumCellWidths[i], line_horizontal), connection_right));
						else if (i == 0)
							formattedTable.Append(String.Format("{0}{1}", connection_left, String.Empty.PadRight(maximumCellWidths[i], line_horizontal)));
						else if (i == maximumCells - 1)
						{
							if (i > previousRow.Length)
								formattedTable.AppendLine(String.Format("{0}{1}{2}", connection_top, String.Empty.PadRight(maximumCellWidths[i], line_horizontal), connection_top_right));
							else if (i > nextRow.Length)
								formattedTable.AppendLine(String.Format("{0}{1}{2}", connection_bottom, String.Empty.PadRight(maximumCellWidths[i], line_horizontal), connection_bottom_right));
							else if (i > previousRow.Length - 1)
								formattedTable.AppendLine(String.Format("{0}{1}{2}", connection, String.Empty.PadRight(maximumCellWidths[i], line_horizontal), connection_top_right));
							else if (i > nextRow.Length - 1)
								formattedTable.AppendLine(String.Format("{0}{1}{2}", connection, String.Empty.PadRight(maximumCellWidths[i], line_horizontal), connection_bottom_right));
							else
								formattedTable.AppendLine(String.Format("{0}{1}{2}", connection, String.Empty.PadRight(maximumCellWidths[i], line_horizontal), connection_right));
						}
						else
						{
							if (i > previousRow.Length)
								formattedTable.Append(String.Format("{0}{1}", connection_top, String.Empty.PadRight(maximumCellWidths[i], line_horizontal)));
							else if (i > nextRow.Length)
								formattedTable.Append(String.Format("{0}{1}", connection_bottom, String.Empty.PadRight(maximumCellWidths[i], line_horizontal)));
							else
								formattedTable.Append(String.Format("{0}{1}", connection, String.Empty.PadRight(maximumCellWidths[i], line_horizontal)));
						}
					}
				}

				rowIndex++;
			}

			for (int i = 0; i < previousRow.Length; i++)
			{
				if (i == 0 && i == previousRow.Length - 1)
					formattedTable.AppendLine(String.Format("{0}{1}{2}", connection_bottom_left, String.Empty.PadRight(maximumCellWidths[i], line_horizontal), connection_bottom_right));
				else if (i == 0)
					formattedTable.Append(String.Format("{0}{1}", connection_bottom_left, String.Empty.PadRight(maximumCellWidths[i], line_horizontal)));
				else if (i == previousRow.Length - 1)
					formattedTable.AppendLine(String.Format("{0}{1}{2}", connection_bottom, String.Empty.PadRight(maximumCellWidths[i], line_horizontal), connection_bottom_right));
				else
					formattedTable.Append(String.Format("{0}{1}", connection_bottom, String.Empty.PadRight(maximumCellWidths[i], line_horizontal)));
			}

			return formattedTable.ToString();
		}

	}
}

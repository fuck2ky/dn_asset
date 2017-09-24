//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.8784
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace XTable {
    
    
    public class FashionList : CVSReader {
        
        public class RowData{
			public uint ItemID;
			public string ItemName;
			public int EquipPos;
			public int Profession;
			public int level;
			public string quality;
			public string DirectoryComment;
			public string ModelPrefabWarrior;
			public string ModelPrefabArcher;
			public string ModelPrefabSorcer;
			public string ModelPrefabCleric;
			public string ModelPrefab5;
			public string ModelPrefab6;
			public string ModelPrefab7;
			public string ModelPrefab8;
			public string PresentID;
			public int[] ReplaceID;
			public string AgainReplaceID;
		}


		public RowData[] Table { get { return table; } }

		private RowData[] table = null;

		public override string bytePath { get { return "Table/FashionList"; } }
        
        public override void OnClear(int lineCount) {
			if (lineCount > 0) table = new RowData[lineCount];
			else table = null;
        }
        
        public override void ReadLine(System.IO.BinaryReader reader) {
			RowData row = new RowData();
			Read<uint>(reader, ref row.ItemID, uintParse); columnno = 0;
			Read<string>(reader, ref row.ItemName, stringParse); columnno = 1;
			Read<int>(reader, ref row.EquipPos, intParse); columnno = 2;
			Read<int>(reader, ref row.Profession, intParse); columnno = 3;
			Read<int>(reader, ref row.level, intParse); columnno = 4;
			Read<string>(reader, ref row.quality, stringParse); columnno = 5;
			Read<string>(reader, ref row.DirectoryComment, stringParse); columnno = 6;
			Read<string>(reader, ref row.ModelPrefabWarrior, stringParse); columnno = 7;
			Read<string>(reader, ref row.ModelPrefabArcher, stringParse); columnno = 8;
			Read<string>(reader, ref row.ModelPrefabSorcer, stringParse); columnno = 9;
			Read<string>(reader, ref row.ModelPrefabCleric, stringParse); columnno = 10;
			Read<string>(reader, ref row.ModelPrefab5, stringParse); columnno = 11;
			Read<string>(reader, ref row.ModelPrefab6, stringParse); columnno = 12;
			Read<string>(reader, ref row.ModelPrefab7, stringParse); columnno = 13;
			Read<string>(reader, ref row.ModelPrefab8, stringParse); columnno = 14;
			Read<string>(reader, ref row.PresentID, stringParse); columnno = 15;
			ReadArray<int>(reader, ref row.ReplaceID, intParse); columnno = 16;
			Read<string>(reader, ref row.AgainReplaceID, stringParse); columnno = 17;
			Table[lineno] = row;
			columnno = -1;
        }
    }
}
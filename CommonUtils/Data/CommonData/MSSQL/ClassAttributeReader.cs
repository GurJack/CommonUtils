﻿﻿using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.Entity;
using System.Reflection;
//using CommonUtils.Attributes;

namespace CommonUtils.MSSQL
{
    public class ClassAttributeReader<T>  : IDataReader where T : class
    {
        // Поля закомментированы, так как функциональность не реализована
        // private readonly Func<object, object>[] _convertTable;
        // private readonly Func<object, bool>[] _constraintsTable;
        // private readonly List<T> _values;
        // private readonly string _tableName;
        // private readonly int _fieldCount;
        private int _currentPos;
        // private readonly Dictionary<int,PropertyInfo> _properties;

        public ClassAttributeReader(List<T> values, string tableName, Func<object, bool>[] constraintsTable = null, Func<object, object>[] convertTable= null)
        {
            //_constraintsTable = constraintsTable;
            //_tableName = tableName;
            ////Пример использования
            ////var constraintsTable = new Func<string, bool>[4];
            ////constraintsTable[0] = x => !string.IsNullOrEmpty(x);
            ////constraintsTable[1] = constraintsTable[0];
            ////constraintsTable[2] = x => true;
            ////constraintsTable[3] = x => true;

            //_convertTable = convertTable;
            ////Пример использования
            ////var convertTable = new Func<object, object>[4];

            ////// Функция преобразования первого столбца csv файла (фамилия)
            ////convertTable[0] = x => x;
            ////// Второго (имя)
            ////convertTable[1] = x => x;
            ////// Третьего (дата)
            ////// Разбираем строковое представление даты по определенному формату.
            ////convertTable[2] = x =>
            ////{
            ////    DateTime datetime;
            ////    if (DateTime.TryParseExact(x.ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture,
            ////        DateTimeStyles.None, out datetime))
            ////    {
            ////        return datetime;
            ////    }
            ////    return null;
            ////};
            ////// Четвертого (промо код)
            ////convertTable[3] = x => Convert.ToInt32(x);

            //_values = values;
            //_fieldCount = 0;
            //_currentPos = -1;

            //_properties =new Dictionary<int, PropertyInfo>();
            //foreach (var info in typeof(T).GetProperties())
            //{
            //    var attr = info.GetCustomAttribute<DataReaderAttribute>();
            //    if (attr == null || (attr.TableName != null && attr.TableName.ToLower() != _tableName.ToLower()))
            //        return;
            //    _fieldCount++;
            //    _properties.Add(attr.Position,info);


            //}

            ////if (fieldCount != null)
            ////    _fieldCount = (int)fieldCount;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string GetName(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public object GetValue(int i)
        {
            // Метод не реализован, так как класс находится в разработке
            throw new NotImplementedException("ClassAttributeReader is under development");

            // Комментарий оригинального кода:
            // return _properties[i].GetValue(_values[_currentPos]);
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public int GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public int FieldCount => 0; // Не реализовано

        public object this[int i] => throw new NotImplementedException();

        public object this[string name] => throw new NotImplementedException();

        public void Close()
        {
           // throw new NotImplementedException();
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            _currentPos++;
            // if (_currentPos >= _values.Count) return false; // Поле _values закомментировано
            return false; // Метод не реализован

            //_currentLine = _streamReader.ReadLine();

            //// В случае, если значения будут содержать символ ";" это работать не будет,
            //// и придется использовать более сложный алгоритм разбора.
            //_currentLineValues = _currentLine.Split(';');

            //var invalidRow = false;
            //for (int i = 0; i < _currentLineValues.Length; i++)
            //{
            //    if (!_constraintsTable[i](_currentLineValues[i]))
            //    {
            //        invalidRow = true;
            //        break;
            //    }
            //}

            //return !invalidRow || Read();
            return true;
        }

        public int Depth { get; }
        public bool IsClosed { get; }
        public int RecordsAffected { get; }

    }
}

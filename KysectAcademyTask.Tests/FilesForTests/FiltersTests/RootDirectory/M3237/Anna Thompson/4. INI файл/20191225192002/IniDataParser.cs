using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OOP_LABS.LABA_4.Model;
using OOP_LABS.LABA_4.Exсeptions;

namespace OOP_LABS.LABA_4.Parser
{
    public class IniDataParser
    {
        #region Initialization
        public IniDataParser()
        {
            Scheme = new IniScheme();
        }
        #endregion

        #region Properties
        public IniScheme Scheme { get; protected set; }
        #endregion

        #region Operations
        public IniData Parse(string iniString)
        {
            return Parse(iniString.Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
        }

        public IniData ParseFile(string path)
        {
            if (!File.Exists(path))
                throw new Exception("Not found file");
            if (path.IndexOf('.') == -1)
                throw new Exception("Invalid file type");
            var type = path.Remove(0, path.LastIndexOf('.') + 1);
            if (type != "ini")
                throw new Exception("Invalid file type");
            return Parse(File.ReadAllText(path));
        }

        public IniData Parse(string[] lines)
        {
            IniData iniData = new IniData();

            _currentSectionNameTemp = null;
            _currentLineNumber = 0;

            foreach (var str in lines)
            {
                _currentLineNumber++;
                _currentLineCopy = str;
                _currentArrayProperty = false;
                ProcessLine(str, iniData);
            }

            return iniData;
        }
        #endregion

        #region Non-public Operations
        private void ProcessLine(string currentLine, IniData iniData)
        {
            if (String.IsNullOrEmpty(currentLine)) return;

            if (ProcessComment(ref currentLine)) return;

            if (ProcessSection(ref currentLine, iniData)) return;

            if (ProcessProperty(ref currentLine, iniData)) return;

            throw new ParsingException("Invalid line", _currentLineNumber, _currentLineCopy);
        }

        private bool ProcessComment(ref string currentLine)
        {
            var commentStartIdx = currentLine.IndexOf(Scheme.CommentString);

            if (commentStartIdx != -1)
            {
                currentLine = currentLine.Remove(commentStartIdx);
            }

            return currentLine.Length <= 0;
        }

        private bool ProcessSection(ref string currentLine, IniData iniData)
        {
            if (currentLine.Length <= 0) return false;

            var sectionStartIdx = currentLine.IndexOf(Scheme.SectionStartString);           

            if (sectionStartIdx == -1) return false;

            var sectionEndRange = currentLine.IndexOf(Scheme.SectionEndString, sectionStartIdx + 1);       

            if (sectionEndRange == -1)
            {
                throw new ParsingException("Invalid section line", _currentLineNumber, _currentLineCopy);
            }

            // Check for Array property
            if (sectionStartIdx == sectionEndRange - Scheme.ArrayString.Length + 1)
            {
                var propertyDeviderPos = currentLine.IndexOf(Scheme.DeviderString);
                if (propertyDeviderPos == sectionEndRange + Scheme.DeviderString.Length) 
                {
                    _currentArrayProperty = true;
                    return false;
                }               
            }

            var startIdx = sectionStartIdx + Scheme.SectionStartString.Length;
            var endIdx = sectionEndRange - Scheme.SectionEndString.Length;
            currentLine = currentLine.Substring(startIdx, endIdx - startIdx + 1);

            var sectionName = currentLine;

            _currentSectionNameTemp = sectionName;

            iniData.Sections.AddSection(sectionName);

            return true;
        }

        private bool ProcessProperty(ref string currentLine, IniData iniData)
        {
            if (currentLine.Length <= 0) return false;

            var propertyDeviderPos = currentLine.IndexOf(Scheme.DeviderString);

            if (propertyDeviderPos == -1) return false;

            var keyStartIdx = 0;
            var ketSize = propertyDeviderPos;

            var valueStartIdx = propertyDeviderPos + 1;
            var valueSize = currentLine.Length - propertyDeviderPos - 1;

            if (_currentArrayProperty)
            {
                ketSize -= Scheme.ArrayString.Length;
            }

            var key = currentLine.Substring(keyStartIdx, ketSize);
            var value = currentLine.Substring(valueStartIdx, valueSize);

            if (String.IsNullOrEmpty(key))
            {
                throw new ParsingException("Invalid property key", _currentLineNumber, _currentLineCopy);
            }

            if (String.IsNullOrEmpty(_currentSectionNameTemp))
            {
                throw new ParsingException("Invalid section name", _currentLineNumber, _currentLineCopy);
            }
            else
            {
                var currentSection = iniData.Sections[_currentSectionNameTemp];

                AddKeyToKeyValueCollection(key, value, currentSection);
            }


            return true;
        }

        private void AddKeyToKeyValueCollection(string key, string value, PropertyCollection keyDataCollection)
        {

            if (keyDataCollection.ContainsKey(key))
            {
                if (!_currentArrayProperty)
                    throw new ParsingException("Not found section name", _currentLineNumber, _currentLineCopy);
                keyDataCollection[key].Value += $",{value}";
            }
            else
            {
                keyDataCollection.AddKey(key, value);
            }

        }
        #endregion

        #region Non-public Members
        private int _currentLineNumber;
        private string _currentLineCopy;
        private string _currentSectionNameTemp;
        private bool _currentArrayProperty;
        #endregion
    }
}

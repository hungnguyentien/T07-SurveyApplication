using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApplication.Utility
{
    public static class Ultils
    {
        public static T DeepCopy<T>(T self)
        {
            var serialized = JsonConvert.SerializeObject(self);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public static string ConvertCapacity(long byteCapacity)
        {
            var lstPow = new List<int> { 3, 2, 1 };
            var lstUnit = new List<string> { "Gb", "Mb", "Kb" };
            var capacity = string.Empty;
            foreach (var itemPow in lstPow.Where(itemPow => byteCapacity >= Math.Pow(1024, itemPow)))
            {
                capacity = Math.Ceiling(byteCapacity / Math.Pow(1024, itemPow)) + lstUnit.ElementAtOrDefault(lstPow.FindIndex(x => x.Equals(itemPow)));
                break;
            }

            return capacity;
        }

        public static async Task<byte[]> ReplaceDocxFile(byte[] content, Dictionary<string, string> dict)
        {
            try
            {
                using var stream = new MemoryStream();
                await stream.WriteAsync(content, 0, content.Length);
                using (var wordDoc = WordprocessingDocument.Open(stream, true))
                {
                    var body = wordDoc.MainDocumentPart?.Document.Body;
                    if (body != null)
                    {
                        foreach (var text in body.Descendants<Text>())
                        foreach (var item in dict.Where(item => text.Text.Equals(item.Key)))
                            text.Text = text.Text.Replace(item.Key, item.Value);

                        var symbolChar = body.Descendants<SymbolChar>().ToList();
                        var getRadioSymbol = symbolChar.Where(x => x.Parent?.Parent?.Parent?.FirstChild?.Parent?.FirstChild?.ElementAtOrDefault(2)?.GetAttributes().FirstOrDefault().Value == "RadioBox").ToList();
                        var getCheckSymbol = symbolChar.Where(x => x.Parent?.Parent?.Parent?.FirstChild?.Parent?.FirstChild?.ElementAtOrDefault(2)?.GetAttributes().FirstOrDefault().Value == "CheckBox").ToList();
                        //TODO Radiobox replace Char nếu trùng code (title)
                        //t2.Char = "F099";
                        //t2.Char = "F09C";
                        //TODO Checkbox replace Char nếu trùng code (title)
                        //t2.Char = "F0A8";
                        //t2.Char = "F052";

                        wordDoc.MainDocumentPart.Document.Body = body;
                    }

                    wordDoc.Save();
                    wordDoc.Close();
                }

                return stream.ToArray();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

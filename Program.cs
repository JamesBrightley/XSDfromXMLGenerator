using System.Xml.Schema;
using System.Xml;

Console.WriteLine("Starting XML files read");
XmlSchemaSet schemaSet = new XmlSchemaSet();
XmlSchemaInference inference = new XmlSchemaInference();
var path = Path.Combine(Directory.GetCurrentDirectory(), @"input-xml");
string output = Path.Combine(Directory.GetCurrentDirectory(), @"output-xsd/generated.xsd");

var firstFile = true;
foreach (string file in Directory.EnumerateFiles(path, "*.xml"))
{
    var reader = XmlReader.Create(file);
    schemaSet = firstFile ? inference.InferSchema(reader) : inference.InferSchema(reader, schemaSet);
    firstFile = false;
}

var xmlWriterSettings = new XmlWriterSettings()
{
    Indent = true,
    IndentChars = "\t"
};

var writer = XmlWriter.Create(output, xmlWriterSettings);

foreach (XmlSchema schema in schemaSet.Schemas())
{
    schema.Write(writer);
}
Console.WriteLine("Finished, wrote file to {0}...", output);
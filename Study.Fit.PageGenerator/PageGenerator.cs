using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Study.Fit.PageGenerator
{

    /// <summary>
    /// Build pages according to given assembly fixture types.
    /// </summary>
    public class PageGenerator
    {
        private string _fitnessRootPath;
        private Assembly _assembly;
        private static readonly string _propertiesFileContent;
        private readonly string _assemblyFilename;

        static PageGenerator()
        {
            _propertiesFileContent = new StreamReader(typeof(PageGenerator).Assembly.GetManifestResourceStream("Study.Fit.PageGenerator.Template.properties.xml")).ReadToEnd();
            Debug.Assert(_propertiesFileContent != null, "properties file content not found");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageGenerator"/> class.
        /// </summary>
        /// <param name="assemblyFilePath">The assembly file path where fixture will be searched.</param>
        /// <param name="fitnessRootPath">The fitness root path.</param>
        public PageGenerator(string assemblyFilePath, string fitnessRootPath)
        {
            _assemblyFilename = Path.GetFileName(assemblyFilePath);
            Initialize(LoadAssembly(assemblyFilePath), fitnessRootPath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageGenerator"/> class.
        /// </summary>
        /// <param name="assembly">The assembly where fixture will be searched.</param>
        /// <param name="fitnessRootPath">The fitness root path.</param>
        public PageGenerator(Assembly assembly, string fitnessRootPath)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            _assemblyFilename = Path.GetFileName(assembly.Location);
            Initialize(assembly, fitnessRootPath);
        }

        private void Initialize(Assembly assembly, string fitnessRootPath)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            if (fitnessRootPath == null) throw new ArgumentNullException("fitnessRootPath");
            _fitnessRootPath = fitnessRootPath;
            _assembly = assembly;
        }

        private Assembly LoadAssembly(string assemblyFilePath)
        {
            byte[] assemblyContent = File.ReadAllBytes(assemblyFilePath);
            return Assembly.ReflectionOnlyLoad(assemblyContent);
        }

        /// <summary>
        /// Generates the pages for all type in assembly.
        /// </summary>
        public void GeneratePages()
        {
            GeneratePages(t => true);
        }

        /// <summary>
        /// Generates the pages for each type which match provided filter.
        /// </summary>
        /// <param name="typeFilter">The type filter.</param>
        public void GeneratePages(Func<Type, bool> typeFilter)
        {
            if (typeFilter == null) throw new ArgumentNullException("typeFilter");
            string rootPath = Path.Combine(_fitnessRootPath, BuildFolderName(_assembly));
            CreatePageFolder(rootPath, GeneratePage(_assembly));
            foreach (Type type in _assembly.GetTypes().Where(t=>!t.IsGenericType).Where(typeFilter))
            {
                string contentFileContent = GeneratePage(type);
                if (contentFileContent != null)
                {
                    CreatePageFolder(Path.Combine(rootPath, type.Name), contentFileContent);
                }
            }
        }

        private static void CreatePageFolder(string folder, string contentFileContent)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            CreateContentFile(folder, contentFileContent);
            CreatePropertiesFile(folder);
        }

        private static void CreateContentFile(string folder, string content)
        {
            CreateFile(folder, "content.txt", content);
        }

        private static void CreatePropertiesFile(string folder)
        {
            CreateFile(folder, "properties.xml", _propertiesFileContent);
        }

        private static void CreateFile(string folder, string filename, string fileContent)
        {
            using (var contentFile = new StreamWriter(Path.Combine(folder, filename)))
            {
                contentFile.Write(fileContent);
                contentFile.Close();
            }
        }

        private static string BuildFolderName(Assembly assembly)
        {
            string folderName = assembly.GetName().Name.Replace('.', ' ');
            var letters = new List<char>();
            bool isNewWord = true;
            foreach (char c in folderName)
            {
                switch (c)
                {
                    case ' ':
                        isNewWord = true;
                        break;
                    default:
                        if (isNewWord)
                        {
                            letters.Add(Char.ToUpper(c));
                            isNewWord = false;
                        }
                        else
                        {
                            letters.Add(c);
                        }
                        break;
                }
            }
            return new string(letters.ToArray());
        }

        private string GeneratePage(Assembly assembly)
        {
            var content = new StringBuilder();
            content.AppendLine("|import|");
            string name;
            if (String.IsNullOrEmpty(_assemblyFilename))
            {
                name = assembly.GetName().Name + ".dll";
            }
            else
            {
                name = _assemblyFilename;
            }
            content.AppendLine("|Libraries\\" + Path.GetFileNameWithoutExtension(name) + "\\" + name + "|");
            content.AppendLine("!contents");
            return content.ToString();
        }

        private string GeneratePage(Type type)
        {
            string content = null;
            PropertyInfo[] properties = type.GetProperties();
            if (properties.Length > 0)
            {
                var contentBuilder = new StringBuilder();
                contentBuilder.AppendLine("!note here is a sample to use the fixture '" + type.Name + "'");
                contentBuilder.AppendLine(String.Empty);
                string tableHeader = BuildTableHeader(properties);
                string tableSampleRow = BuildTableSampleRow(properties);
                ConstructorInfo[] constructors = type.GetConstructors();
                foreach (ConstructorInfo constructor in constructors)
                {
                    contentBuilder.Append("|" + ToFitname(type.Name) + "|");
                    foreach (ParameterInfo parameter in constructor.GetParameters())
                    {
                        contentBuilder.AppendFormat("<a {0} value>|", ToFitname(parameter.ParameterType));
                    }
                    contentBuilder.AppendLine(String.Empty);
                    contentBuilder.AppendLine(tableHeader);
                    contentBuilder.AppendLine(tableSampleRow);
                    contentBuilder.AppendLine(String.Empty);
                }
                content = contentBuilder.ToString();
            }
            return content;
        }

        private string BuildTableSampleRow(PropertyInfo[] properties)
        {
            var headers = new StringBuilder("|");
            foreach (PropertyInfo property in properties)
            {
                if (HasAPublicSetter(property))
                {
                    headers.AppendFormat("<a {0} value>|", ToFitname(property.PropertyType));
                }
                else if (HasAPublicGetter(property))
                {
                    headers.AppendFormat("<an expected {0} result>|", ToFitname(property.PropertyType));
                }
            }
            return headers.ToString();
        }

        private static bool HasAPublicGetter(PropertyInfo property)
        {
            return property.GetAccessors().FirstOrDefault(m => m.Name.StartsWith("get") && m.IsPublic) != null;
        }

        private static bool HasAPublicSetter(PropertyInfo property)
        {
            return property.GetAccessors().FirstOrDefault(m => m.Name.StartsWith("set") && m.IsPublic) != null;
        }

        private string BuildTableHeader(PropertyInfo[] properties)
        {
            var headers = new StringBuilder("|");
            foreach (PropertyInfo property in properties)
            {
                if (HasAPublicSetter(property))
                {
                    headers.AppendFormat("{0}|", ToFitname(property.Name));
                }
                else if (HasAPublicGetter(property))
                {
                    headers.AppendFormat("{0}?|", ToFitname(property.Name));                    
                }
            }
            return headers.ToString();
        }

        private string ToFitname(string name)
        {
            var result = new StringBuilder();
            foreach (char c in name)
            {
                if (Char.IsUpper(c))
                    result.Append(' ');
                result.Append(c);
            }
            return result.ToString().Trim().ToLower();
        }

        private string ToFitname(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return ToFitname(type.GetGenericArguments()[0]);
            }
            else return type.Name.ToLower();
        }
    }
}
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Json
{
    public class BlockSurrogate : IDataContractSurrogate
    {

        public Type GetDataContractType(Type type)
        {
            if (type == typeof(byte[][]))
                return typeof(Dictionary<string, string>);
            return type;
        }

        static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        static string ByteArrayToString(byte[] bytes)
        {
            return string.Join("", bytes.Select(b => b.ToString("X2")));
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            if (obj.GetType() == typeof(Dictionary<string, string>) && targetType == typeof(byte[][]))
            {
                var dic = obj as Dictionary<string, string>;
                var bytes = new byte[dic.Count][];
                for (int i = 0; i < dic.Count; i++)
                    bytes[i] = StringToByteArray(dic[i.ToString()]);
                return bytes;
            }
            return obj;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            if (obj.GetType() == typeof(byte[][]) && targetType == typeof(Dictionary<string, string>))
            {
                var bytes = obj as byte[][];
                var dic = new Dictionary<string, string>();
                for (int i = 0; i < bytes.Length; i++)
                    dic[i.ToString()] = ByteArrayToString(bytes[i]);
                return dic;
            }
            return obj;
        }

        // ------- The rest of these methods are not needed -------
        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException();
        }
    }
}

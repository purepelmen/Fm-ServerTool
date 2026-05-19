using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fm_ServerTool.Model
{
    [JsonSerializable(typeof(WebData))]
    [JsonSerializable(typeof(GameBuild))]
    internal partial class ModelJsonContext : JsonSerializerContext
    { 
    }
}

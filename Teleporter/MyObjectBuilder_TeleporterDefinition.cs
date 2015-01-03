using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_TeleporterDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        [ProtoMember(3)]
        public Vector4[] ButtonColors;
        [ProtoMember(1)]
        public int ButtonCount;
        [ProtoMember(2)]
        public string[] ButtonSymbols;
        [ProtoMember(4)]
        public Vector4 UnassignedButtonColor;

        public MyObjectBuilder_TeleporterDefinition()
        {

        }

    }
}

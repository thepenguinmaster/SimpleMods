using Sandbox.Common.ObjectBuilders.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRageMath;


namespace Sandbox.Definitions
{
    [MyDefinitionType(typeof(MyObjectBuilder_TeleporterDefinition))]
    public class MyTeleporterDefinition : MyCubeBlockDefinition
    {
        public Vector4[] ButtonColors;
        public int ButtonCount;
        public string[] ButtonSymbols;
        public Vector4 UnassignedButtonColor;

        public MyTeleporterDefinition(){}

        public override void Init(MyObjectBuilder_DefinitionBase builder){}
    }
}
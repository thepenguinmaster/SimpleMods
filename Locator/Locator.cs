using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces;
using Sandbox.ModAPI.Ingame;
using Sandbox.Game;

using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.Components;
using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using System.IO;

namespace scripts.ThePenuginMaster
{

    // You know, if your gonna copy, why not give some credit? 
    [Sandbox.Common.MySessionComponentDescriptor(Sandbox.Common.MyUpdateOrder.BeforeSimulation)]
    class Locator : Sandbox.Common.MySessionComponentBase
    {
        private bool _IsLoaded = false;
                private bool _DisplayLocation = false;

        public override void UpdateBeforeSimulation()
        {
            if (!_IsLoaded)
            {
                init();
            }

            //if (_DisplayLocation)
            //{
            //    MyAPIGateway.Utilities.ShowNotification( "Mod Loaded: Locator",);
            //}
        }

        public void init()
        {
            _IsLoaded = true;
            MyAPIGateway.Utilities.MessageEntered += Utilities_MessageEntered;
        }

        private void Utilities_MessageEntered(string messageText, ref bool sendToOthers)
        {
            if (!messageText.StartsWith("/")) { return; }
            List<string> words = new List<string>(messageText.Trim().ToLower().Replace("/", "").Split(' '));

            sendToOthers = false;
            //messageText = "";

            switch (words[0])
            {
                case "report":
                    MyAPIGateway.Utilities.ShowMessage("HAL", "Mod Loaded: Teleport");
                    sendToOthers = false;
                    break;
                case "hello":
                    SayHello(words);
                    sendToOthers = false;
                    break;

                case "reset":
                    Reset();
                    sendToOthers = false;
                    break;

                case "location":
                    ReportLocation();
                    sendToOthers = false;
                    break;

                case "tp":
                    Teleport(words);
                    sendToOthers = false;
                    break;

                case "help":
                    sendToOthers = false;
                    break;
            }
        }

        void Reset()
        {
            _IsLoaded = false;
            MyAPIGateway.Utilities.ShowMessage("HAL", "Good bye Dave");
        }

        void SayHello(List<string> words)
        {
            switch (words[1])
            {
                case "hal":
                    MyAPIGateway.Utilities.ShowMessage("HAL", "Hello Dave.");
                    break;
                case "scotty":
                    MyAPIGateway.Utilities.ShowMessage("Scotty", "I'm giving her all she's got, Captain!");
                    break;
                case "cortana":
                    MyAPIGateway.Utilities.ShowMessage("Scotty", "One moment, sir. Accessing Covenant Battlenet.");
                    break;
            }
        }

        void ReportLocation()
        {
            string locationString = "X: " + MyAPIGateway.Session.Player.PlayerCharacter.Entity.GetPosition().X + " Y: " + MyAPIGateway.Session.Player.PlayerCharacter.Entity.GetPosition().Y + " Z: " + MyAPIGateway.Session.Player.PlayerCharacter.Entity.GetPosition().Z;
            MyAPIGateway.Utilities.ShowMessage("HAL", locationString);
            //_DisplayLocation = true;
        }


        // Im watching you
        void Teleport(List<string> words)
        {
            IMyEntity entityToMove;

            // teleport the player to location
            if (words.Count == 4)
            {
                float x, y, z;
                string xs = words[1];
                string ys = words[2];
                string zs = words[3];
                MyAPIGateway.Utilities.ShowMessage("Scotty", "Locking on to target.. ");

                if (Single.TryParse(xs, out x) && Single.TryParse(ys, out y) && Single.TryParse(zs, out z))
                {
                    entityToMove = MyAPIGateway.Session.Player.PlayerCharacter.Entity;
                    if (entityToMove.Parent != null)
                    {
                        MyAPIGateway.Utilities.ShowMessage("Scotty", "Teleporting Ship");
                        entityToMove = entityToMove.GetTopMostParent();
                        var worldMatrix = entityToMove.WorldMatrix;
                        entityToMove.SetPosition(new VRageMath.Vector3(x, y, z));
                        //var cubeGrid = MyAPIGateway.Session.Player.PlayerCharacter.Entity.GetTopMostParent();
                        //cubeGrid.SetPosition(new VRageMath.Vector3(x, y, z));
                    }
                    else
                    {
                        MyAPIGateway.Utilities.ShowMessage("Scotty", "Teleporting player");
                        entityToMove.SetPosition(new VRageMath.Vector3(x, y, z));
                    }
                    MyAPIGateway.Utilities.ShowMessage("Scotty", "Teleport complete");
                }
                else
                {
                    MyAPIGateway.Utilities.ShowMessage("Scotty", "Sorry Captain, that location is no good.");
                }
            }
            else if (words.Count == 1)
            {
                // tp to cursor
                // get player entity
                MyAPIGateway.Utilities.ShowMessage("Scotty", "Sorry Captain, teleport to point is not supported. Give me the coordinates to send you to.");
            }
            else
            {
                MyAPIGateway.Utilities.ShowMessage("Scotty", "Wrong number of arguments");
            }
        }

        protected override void UnloadData()
        {
            MyAPIGateway.Utilities.MessageEntered -= Utilities_MessageEntered;
            base.UnloadData();
        }
    }
}
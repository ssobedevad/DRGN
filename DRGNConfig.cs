﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;

namespace DRGN
{
	
	public class DRGNConfig : ModConfig
	{
		
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[Label("Disable Yoyo AI")]		
		public bool DisableYoyoAI { get; set; }		
	}
}
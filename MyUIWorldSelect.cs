using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Exceptions;
using Terraria.ModLoader.IO;
using Terraria.UI;
using Terraria.Utilities;

namespace DRGN
{
    internal class MyUIWorldSelect : UIState
	{
		public UIList _worldList;
		public override void OnInitialize()
		{
			UIElement uIElement = new UIElement();
			uIElement.Width.Set(0f, 0.8f);
			uIElement.MaxWidth.Set(600f, 0f);
			uIElement.Top.Set(220f, 0f);
			uIElement.Height.Set(-220f, 1f);
			uIElement.HAlign = 0.5f;
			UIPanel uIPanel = new UIPanel();
			uIPanel.Width.Set(0f, 1f);
			uIPanel.Height.Set(-110f, 1f);
			uIPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			uIElement.Append(uIPanel);
			_worldList = new UIList();
			_worldList.Width.Set(-25f, 1f);
			_worldList.Height.Set(0f, 1f);
			_worldList.ListPadding = 5f;
			uIPanel.Append(_worldList);
			UIScrollbar uIScrollbar = new UIScrollbar();
			uIScrollbar.SetView(100f, 1000f);
			uIScrollbar.Height.Set(0f, 1f);
			uIScrollbar.HAlign = 1f;
			uIPanel.Append(uIScrollbar);
			_worldList.SetScrollbar(uIScrollbar);
			UITextPanel<string> uITextPanel = new UITextPanel<string>("Select World", 0.8f, true);
			uITextPanel.HAlign = 0.5f;
			uITextPanel.Top.Set(-35f, 0f);
			uITextPanel.SetPadding(15f);
			uITextPanel.BackgroundColor = new Color(73, 94, 171);
			uIElement.Append(uITextPanel);
			UITextPanel<string> uITextPanel2 = new UITextPanel<string>("Back", 0.7f, true);
			uITextPanel2.Width.Set(-10f, 0.5f);
			uITextPanel2.Height.Set(50f, 0f);
			uITextPanel2.VAlign = 1f;
			uITextPanel2.Top.Set(-45f, 0f);
			uITextPanel2.OnMouseOver += new UIElement.MouseEvent(FadedMouseOver);
			uITextPanel2.OnMouseOut += new UIElement.MouseEvent(FadedMouseOut);
			uITextPanel2.OnClick += new UIElement.MouseEvent(GoBackClick);
			uIElement.Append(uITextPanel2);
			UITextPanel<string> uITextPanel3 = new UITextPanel<string>("New", 0.7f, true);
			uITextPanel3.CopyStyle(uITextPanel2);
			uITextPanel3.HAlign = 1f;
			uITextPanel3.OnMouseOver += new UIElement.MouseEvent(FadedMouseOver);
			uITextPanel3.OnMouseOut += new UIElement.MouseEvent(FadedMouseOut);
			uITextPanel3.OnClick += new UIElement.MouseEvent(NewWorldClick);
			uIElement.Append(uITextPanel3);
			base.Append(uIElement);
		}
		private void NewWorldClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.PlaySound(10, -1, -1, 1);
			Main.menuMode = 16;
			Main.newWorldName = Lang.gen[57] + " " + (Main.WorldList.Count + 1);
		}
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.PlaySound(11, -1, -1, 1);
			Main.menuMode = (Main.menuMultiplayer ? 12 : 1);
		}
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.PlaySound(12, -1, -1, 1);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
		}
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
		}
		public override void OnActivate()
		{
			Main.LoadWorlds();
			_worldList.Clear();
			foreach (WorldFileData current in Main.WorldList)
			{
				bool mental = false;
				if (DRGN.worldInfo.ContainsKey(current.Name)) { mental = DRGN.worldInfo[current.Name]; }
				else
				{
					mental = CheckMentalMode(Main.WorldPath + "/" + current.Name + ".twld", current.IsCloudSave);
				}
				_worldList.Add(new MyUIWorldListItem(current,mental));
			}
		}
		private bool CheckMentalMode(string path, bool isCloudSave)
		{

			path = Path.ChangeExtension(path, ".twld");
			if (!FileUtilities.Exists(path, isCloudSave))
				return false;

			var buf = FileUtilities.ReadAllBytes(path, isCloudSave);
			var tag = TagIO.FromStream(new MemoryStream(buf));
			return LoadModData(tag.GetList<TagCompound>("modData"));

		}
		private bool LoadModData(IList<TagCompound> list)
		{
			foreach (TagCompound tag in list)
			{
				if (tag.GetString("mod") == "DRGN" && tag.GetString("name") == "DRGNModWorld")
				{
					var mod = ModLoader.GetMod(tag.GetString("mod"));

					var modWorld = mod?.GetModWorld(tag.GetString("name"));
					if (modWorld != null)
					{
						try
						{
							if (tag.ContainsKey("legacyData"))
								modWorld.LoadLegacy(new BinaryReader(new MemoryStream(tag.GetByteArray("legacyData"))));
							else
								return tag.GetCompound("data").GetBool("MentalMode"); 
						}
						catch (Exception e)
						{
							throw new CustomModDataException(mod,
								"Error in reading custom world data for " + mod.Name, e);
						}
					}
					if (DRGNModWorld.MentalMode)
					{
						return true;
					}

				}

			}
			return false;
		}
	}
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Tiles
{
	public class MonsterBanners : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.StyleWrapLimit = 111;
			TileObjectData.addTile(Type);
			dustType = -1;
			disableSmartCursor = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Banner");
			AddMapEntry(new Color(13, 88, 130), name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			int style = frameX / 18;
			string item;
			switch (style)
			{
				case 0:
					item = "RockSlimeBanner";
					break;
				case 1:
					item = "AmberSlimeBanner";
					break;
				case 2:
					item = "AmethystSlimeBanner";
					break;
				case 3:
					item = "DiamondSlimeBanner";
					break;
				case 4:
					item = "EmeraldSlimeBanner";
					break;
				case 5:
					item = "RubySlimeBanner";
					break;
				case 6:
					item = "SapphireSlimeBanner";
					break;
				case 7:
					item = "TopazSlimeBanner";
					break;
				case 8:
					item = "AntBanner";
					break;
				case 9:
					item = "FireAntBanner";
					break;
				case 10:
					item = "ElectricAntBanner";
					break;
				case 11:
					item = "FlyingAntBanner";
					break;
				case 12:
					item = "VoidCrawlerBanner";
					break;
				case 13:
					item = "GlacialCrawlerBanner";
					break;
				case 14:
					item = "AntCrawlerBanner";
					break;
				case 15:
					item = "LunarWyrmBanner";
					break;
				case 16:
					item = "MagmaticCrawlerBanner";
					break;
				case 17:
					item = "SnakobBanner";
					break;
				case 18:
					item = "DragonFlyBanner";
					break;
				case 19:
					item = "BloodReaperBanner";
					break;

				default:
					return;
			}
			Item.NewItem(i * 16, j * 16, 16, 48, mod.ItemType(item));
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (closer)
			{
				Player player = Main.LocalPlayer;
				int style = Main.tile[i, j].frameX / 18;
				string type;
				switch (style)
				{
					case 0:
						type = "RockSlime";
						break;
					case 1:
						type = "AmberSlime";

						break;
					case 2:
						type = "AmethystSlime";

						break;
					case 3:
						type = "DiamondSlime";
						break;
					case 4:
						type = "EmeraldSlime";
						break;
					case 5:
						type = "RubySlime";
						break;
					case 6:
						type = "SapphireSlime";
						break;
					case 7:
						type = "TopazSlime";
						break;
					case 8:
						type = "Ant";
						break;
					case 9:
						type = "FireAnt";
						break;
					case 10:
						type = "ElectricAnt";
						break;
					case 11:
						type = "FlyingAnt";
						break;
					case 12:
						type = "VoidCrawler";
						break;
					case 13:
						type = "GlacialCrawler";
						break;
					case 14:
						type = "AntCrawler";
						break;
					case 15:
						type = "LunarWyrm";
						break;
					case 16:
						type = "MagmaticCrawler";
						break;
					case 17:
						type = "Snakob";
						break;
					case 18:
						type = "DragonFlyMini";
						break;
					case 19:
						type = "Bloodreaper";
						break;

					default:
						return;
				}
				player.NPCBannerBuff[mod.NPCType(type)] = true;
				player.hasBanner = true;
			}
		}

		public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
		{
			if (i % 2 == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
		}
	}
}
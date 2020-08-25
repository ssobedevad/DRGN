using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Graphics;
using Terraria.IO;
using Terraria.Social;
using Terraria.UI;
namespace DRGN
{
	internal class MyUIWorldListItem : UIPanel
	{
		public bool MentalMode;
		public WorldFileData _data;
		public Texture2D _dividerTexture;
		public Texture2D _innerPanelTexture;
		public UIImage _worldIcon;
		public UIText _buttonLabel;
		public UIText _deleteButtonLabel;
		public Texture2D _buttonCloudActiveTexture;
		public Texture2D _buttonCloudInactiveTexture;
		public Texture2D _buttonFavoriteActiveTexture;
		public Texture2D _buttonFavoriteInactiveTexture;
		public Texture2D _buttonPlayTexture;
		public Texture2D _buttonDeleteTexture;
		public UIImageButton _deleteButton;
		public bool IsFavorite
		{
			get
			{
				return _data.IsFavorite;
			}
		}
		public MyUIWorldListItem(WorldFileData data , bool mentalMode)
		{
			MentalMode = mentalMode;
			BorderColor = new Color(89, 116, 213) * 0.7f;
			_dividerTexture = TextureManager.Load("Images/UI/Divider");
			_innerPanelTexture = TextureManager.Load("Images/UI/InnerPanelBackground");
			_buttonCloudActiveTexture = TextureManager.Load("Images/UI/ButtonCloudActive");
			_buttonCloudInactiveTexture = TextureManager.Load("Images/UI/ButtonCloudInactive");
			_buttonFavoriteActiveTexture = TextureManager.Load("Images/UI/ButtonFavoriteActive");
			_buttonFavoriteInactiveTexture = TextureManager.Load("Images/UI/ButtonFavoriteInactive");
			_buttonPlayTexture = TextureManager.Load("Images/UI/ButtonPlay");
			_buttonDeleteTexture = TextureManager.Load("Images/UI/ButtonDelete");
			Height.Set(96f, 0f);
			Width.Set(0f, 1f);
			base.SetPadding(6f);
			_data = data;
			_worldIcon = new UIImage(GetIcon());
			_worldIcon.Left.Set(4f, 0f);
			_worldIcon.OnDoubleClick += new UIElement.MouseEvent(PlayGame);
			base.Append(_worldIcon);
			UIImageButton uIImageButton = new UIImageButton(_buttonPlayTexture);
			uIImageButton.VAlign = 1f;
			uIImageButton.Left.Set(4f, 0f);
			uIImageButton.OnClick += new UIElement.MouseEvent(PlayGame);
			base.OnDoubleClick += new UIElement.MouseEvent(PlayGame);
			uIImageButton.OnMouseOver += new UIElement.MouseEvent(PlayMouseOver);
			uIImageButton.OnMouseOut += new UIElement.MouseEvent(ButtonMouseOut);
			base.Append(uIImageButton);
			UIImageButton uIImageButton2 = new UIImageButton(_data.IsFavorite ? _buttonFavoriteActiveTexture : _buttonFavoriteInactiveTexture);
			uIImageButton2.VAlign = 1f;
			uIImageButton2.Left.Set(28f, 0f);
			uIImageButton2.OnClick += new UIElement.MouseEvent(FavoriteButtonClick);
			uIImageButton2.OnMouseOver += new UIElement.MouseEvent(FavoriteMouseOver);
			uIImageButton2.OnMouseOut += new UIElement.MouseEvent(ButtonMouseOut);
			uIImageButton2.SetVisibility(1f, _data.IsFavorite ? 0.8f : 0.4f);
			base.Append(uIImageButton2);
			if (SocialAPI.Cloud != null)
			{
				UIImageButton uIImageButton3 = new UIImageButton(_data.IsCloudSave ? _buttonCloudActiveTexture : _buttonCloudInactiveTexture);
				uIImageButton3.VAlign = 1f;
				uIImageButton3.Left.Set(52f, 0f);
				uIImageButton3.OnClick += new UIElement.MouseEvent(CloudButtonClick);
				uIImageButton3.OnMouseOver += new UIElement.MouseEvent(CloudMouseOver);
				uIImageButton3.OnMouseOut += new UIElement.MouseEvent(ButtonMouseOut);
				base.Append(uIImageButton3);
			}
			UIImageButton uIImageButton4 = new UIImageButton(_buttonDeleteTexture);
			uIImageButton4.VAlign = 1f;
			uIImageButton4.HAlign = 1f;
			uIImageButton4.OnClick += new UIElement.MouseEvent(DeleteButtonClick);
			uIImageButton4.OnMouseOver += new UIElement.MouseEvent(DeleteMouseOver);
			uIImageButton4.OnMouseOut += new UIElement.MouseEvent(DeleteMouseOut);
			_deleteButton = uIImageButton4;
			if (!_data.IsFavorite)
			{
				base.Append(uIImageButton4);
			}
			_buttonLabel = new UIText("", 1f, false);
			_buttonLabel.VAlign = 1f;
			_buttonLabel.Left.Set(80f, 0f);
			_buttonLabel.Top.Set(-3f, 0f);
			base.Append(_buttonLabel);
			_deleteButtonLabel = new UIText("", 1f, false);
			_deleteButtonLabel.VAlign = 1f;
			_deleteButtonLabel.HAlign = 1f;
			_deleteButtonLabel.Left.Set(-30f, 0f);
			_deleteButtonLabel.Top.Set(-3f, 0f);
			base.Append(_deleteButtonLabel);
		}
		private Texture2D GetIcon()
		{
			return TextureManager.Load("Images/UI/Icon" + (_data.IsHardMode ? "Hallow" : "") + (_data.HasCorruption ? "Corruption" : "Crimson"));
		}
		private void FavoriteMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (_data.IsFavorite)
			{
				_buttonLabel.SetText("Unfavorite");
				return;
			}
			_buttonLabel.SetText("Favorite");
		}
		private void CloudMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (_data.IsCloudSave)
			{
				_buttonLabel.SetText("Move off cloud");
				return;
			}
			_buttonLabel.SetText("Move to cloud");
		}
		private void PlayMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			_buttonLabel.SetText("Play");
		}
		private void DeleteMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			_deleteButtonLabel.SetText("Delete");
		}
		private void DeleteMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			_deleteButtonLabel.SetText("");
		}
		private void ButtonMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			_buttonLabel.SetText("");
		}
		private void CloudButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (_data.IsCloudSave)
			{
				_data.MoveToLocal();
			}
			else
			{
				_data.MoveToCloud();
			}
			((UIImageButton)evt.Target).SetImage(_data.IsCloudSave ? _buttonCloudActiveTexture : _buttonCloudInactiveTexture);
			if (_data.IsCloudSave)
			{
				_buttonLabel.SetText("Move off cloud");
				return;
			}
			_buttonLabel.SetText("Move to cloud");
		}
		private void DeleteButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			for (int i = 0; i < Main.WorldList.Count; i++)
			{
				if (Main.WorldList[i] == _data)
				{
					Main.PlaySound(10, -1, -1, 1);
					Main.selectedWorld = i;
					Main.menuMode = 9;
					return;
				}
			}
		}
		private void PlayGame(UIMouseEvent evt, UIElement listeningElement)
		{
			if (listeningElement != evt.Target)
			{
				return;
			}
			_data.SetAsActive();
			Main.PlaySound(10, -1, -1, 1);
			Main.GetInputText("");
			if (Main.menuMultiplayer && SocialAPI.Network != null)
			{
				Main.menuMode = 889;
			}
			else if (Main.menuMultiplayer)
			{
				Main.menuMode = 30;
			}
			else
			{
				Main.menuMode = 10;
			}
			if (!Main.menuMultiplayer)
			{
				WorldGen.playWorld();
			}
		}
		private void FavoriteButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			_data.ToggleFavorite();
			((UIImageButton)evt.Target).SetImage(_data.IsFavorite ? _buttonFavoriteActiveTexture : _buttonFavoriteInactiveTexture);
			((UIImageButton)evt.Target).SetVisibility(1f, _data.IsFavorite ? 0.8f : 0.4f);
			if (_data.IsFavorite)
			{
				_buttonLabel.SetText("Unfavorite");
				base.RemoveChild(_deleteButton);
			}
			else
			{
				_buttonLabel.SetText("Favorite");
				base.Append(_deleteButton);
			}
			UIList uIList = Parent.Parent as UIList;
			if (uIList != null)
			{
				uIList.UpdateOrder();
			}
		}
		public override int CompareTo(object obj)
		{
			MyUIWorldListItem MyuIWorldListItem = obj as MyUIWorldListItem;
			if (MyuIWorldListItem == null)
			{
				return base.CompareTo(obj);
			}
			if (IsFavorite && !MyuIWorldListItem.IsFavorite)
			{
				return -1;
			}
			if (!IsFavorite && MyuIWorldListItem.IsFavorite)
			{
				return 1;
			}
			return _data.Name.CompareTo(MyuIWorldListItem._data.Name);
		}
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			BackgroundColor = new Color(73, 94, 171);
			BorderColor = new Color(89, 116, 213);
		}
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			BackgroundColor = new Color(63, 82, 151) * 0.7f;
			BorderColor = new Color(89, 116, 213) * 0.7f;
		}
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
		{
			spriteBatch.Draw(_innerPanelTexture, position, new Rectangle?(new Rectangle(0, 0, 8, _innerPanelTexture.Height)), Color.White);
			spriteBatch.Draw(_innerPanelTexture, new Vector2(position.X + 8f, position.Y), new Rectangle?(new Rectangle(8, 0, 8, _innerPanelTexture.Height)), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(_innerPanelTexture, new Vector2(position.X + width - 8f, position.Y), new Rectangle?(new Rectangle(16, 0, 8, _innerPanelTexture.Height)), Color.White);
		}
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			CalculatedStyle dimensions = _worldIcon.GetDimensions();
			float num = dimensions.X + dimensions.Width;
			Color color = _data.IsValid ? Color.White : Color.Red;
			Utils.DrawBorderString(spriteBatch, _data.Name, new Vector2(num + 6f, dimensions.Y - 2f), color, 1f, 0f, 0f, -1);
			spriteBatch.Draw(_dividerTexture, new Vector2(num, innerDimensions.Y + 21f), null, Color.White, 0f, Vector2.Zero, new Vector2((base.GetDimensions().X + base.GetDimensions().Width - num) / 8f, 1f), SpriteEffects.None, 0f);
			Vector2 vector = new Vector2(num + 6f, innerDimensions.Y + 29f);
			float num2 = 80f;
			DrawPanel(spriteBatch, vector, num2);
			string text = MentalMode ? "Mental" : _data.IsExpertMode ? "Expert" : "Normal";
			float x = Main.fontMouseText.MeasureString(text).X;
			float x2 = num2 * 0.5f - x * 0.5f;
			Utils.DrawBorderString(spriteBatch, text, vector + new Vector2(x2, 3f), MentalMode ? Color.OrangeRed : _data.IsExpertMode ? new Color(217, 143, 244) : Color.White, 1f, 0f, 0f, -1);
			vector.X += num2 + 5f;
			float num3 = 140f;
			DrawPanel(spriteBatch, vector, num3);
			string text2 = _data.WorldSizeName + " World";
			float x3 = Main.fontMouseText.MeasureString(text2).X;
			float x4 = num3 * 0.5f - x3 * 0.5f;
			Utils.DrawBorderString(spriteBatch, text2, vector + new Vector2(x4, 3f), Color.White, 1f, 0f, 0f, -1);
			vector.X += num3 + 5f;
			float num4 = innerDimensions.X + innerDimensions.Width - vector.X;
			DrawPanel(spriteBatch, vector, num4);
			string text3 = "Created: " + _data.CreationTime.ToString("d MMMM yyyy");
			float x5 = Main.fontMouseText.MeasureString(text3).X;
			float x6 = num4 * 0.5f - x5 * 0.5f;
			Utils.DrawBorderString(spriteBatch, text3, vector + new Vector2(x6, 3f), Color.White, 1f, 0f, 0f, -1);
			vector.X += num4 + 5f;
		}
	}
}

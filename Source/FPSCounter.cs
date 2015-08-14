using AverageBuddy;
using FontBuddyLib;
using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;

namespace FrameRateCounter
{
	/// <summary>
	/// This item is a game component that sits and calculates the average FPS
	/// </summary>
	public class FpsCounter : DrawableGameComponent
	{
		#region Properties

		/// <summary>
		/// The contentmanager we are going to use to load our font
		/// </summary>
		private ContentManager Content { get; set; }

		/// <summary>
		/// The spritebatch we will use to draw the fps
		/// </summary>
		private SpriteBatch SpriteBatch { get; set; }

		/// <summary>
		/// Used to draw the fps
		/// </summary>
		private ShadowTextBuddy Font { get; set; }

		/// <summary>
		/// Used to calculate the average FPS
		/// </summary>
		private Averager<int> AverageFps { get; set; }

		/// <summary>
		/// Store the current average frames/second
		/// </summary>
		private int CurrentFps { get; set; }

		/// <summary>
		/// used to compute the fps
		/// </summary>
		private GameClock FpsClock { get; set; }

		#endregion //Properties

		#region Methods

		public FpsCounter(Game game)
			: base(game)
		{
			Content = new ContentManager(game.Services);
			Content.RootDirectory = "Content";
			AverageFps = new Averager<int>(10, 0);
			CurrentFps = 0;
			FpsClock = new GameClock();
		}

		/// <summary>
		/// load all the content required for this dude
		/// </summary>
		protected override void LoadContent()
		{
			base.LoadContent();

			SpriteBatch = new SpriteBatch(GraphicsDevice);
			Font = new ShadowTextBuddy();
			Font.LoadContent(Content, "FpsFont");
		}

		/// <summary>
		/// clean up all our content
		/// </summary>
		protected override void UnloadContent()
		{
			base.UnloadContent();
			Content.Unload();
		}

		/// <summary>
		/// Called every frame to update the FPS
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
		}

		/// <summary>
		/// called every game loop to draw the fps!
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Draw(GameTime gameTime)
		{
			FpsClock.Update(gameTime);

			//Get the number of seconds that have elasped since last frame
			var seconds = FpsClock.TimeDelta;

			//Convert into frames
			var frames = Math.Max(0, (int)(1.0f / seconds));

			//Store in the averager
			CurrentFps = AverageFps.Update(frames);

			//get the text string of the fps
			var fps = string.Format("fps: {0}", CurrentFps);

			//get the location to draw teh fps at
			var pos = new Point(Resolution.TitleSafeArea.Left, Resolution.TitleSafeArea.Bottom);

			//draw the fps!
			SpriteBatch.Begin();
			Font.Write(fps, pos, Justify.Left, 1.0f, Color.White, SpriteBatch, FpsClock);
			SpriteBatch.End();
		}

		#endregion //Methods
	}
}

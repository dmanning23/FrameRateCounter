using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AverageBuddy;
using FontBuddyLib;
using GameTimer;

namespace FrameRateCounter
{
	/// <summary>
	/// This item is a game component that sits and calculates the average FPS
	/// </summary>
	public class FPSCounter : DrawableGameComponent
	{
		#region Members

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
		private Averager<int> AverageFPS { get; set; }

		/// <summary>
		/// Store the current average frames/second
		/// </summary>
		private int CurrentFPS { get; set; }

		/// <summary>
		/// used to compute the fps
		/// </summary>
		private GameClock FpsClock { get; set; }

		#endregion //Members

		#region Methods

		public FPSCounter(Game game)
			: base(game)
		{
			Content = new ContentManager(game.Services);
			AverageFPS = new Averager<int>(120, 0);
			CurrentFPS = 0;
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
			Font.Font = Content.Load<SpriteFont>("FpsFont");
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
			FpsClock.Update(gameTime);

			//Get the number of seconds that have elasped since last frame
			float seconds = FpsClock.TimeDelta;
			
			//Convert into frames
			int frames = (int)(seconds * 60.0f);

			//Store in the averager
			CurrentFPS = AverageFPS.Update(frames);
		}

		/// <summary>
		/// called every game loop to draw the fps!
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Draw(GameTime gameTime)
		{
			//get the text string of the fps
			string fps = string.Format("fps: {0}", CurrentFPS);

			//draw the fps!
			SpriteBatch.Begin();
			Font.Write(fps, new Point(32, 32), Justify.Left, 1.0f, Color.White, SpriteBatch, gameTime.ElapsedGameTime.Seconds);
			SpriteBatch.End();
		}

		#endregion //Methods
	}
}

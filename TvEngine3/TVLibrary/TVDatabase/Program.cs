//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the Gentle.NET Business Entity template, $Rev: 965 $
//========================================================================
using System;
using System.Collections;
using Gentle.Common;
using Gentle.Framework;

namespace TvDatabase
{
	/// <summary>
	/// Instances of this class represent the properties and methods of a row in the table <b>Program</b>.
	/// </summary>
	[TableName("Program")]
	public class Program : Persistent
  {
    Program _currentProgram = null;
    Program _nextProgram = null;

		#region Members
		private bool isChanged;
		[TableColumn("idProgram", NotNull=true), PrimaryKey(AutoGenerated=true)]
		private int idProgram;
		[TableColumn("idChannel", NotNull = true), ForeignKey("Channel", "idChannel")]
		private int idChannel;
		[TableColumn("startTime", NotNull=true)]
		private DateTime startTime;
		[TableColumn("endTime", NotNull=true)]
		private DateTime endTime;
		[TableColumn("title", NotNull=true)]
		private string title;
		[TableColumn("description", NotNull=true)]
		private string description;
		[TableColumn("genre", NotNull=true)]
		private string genre;
		[TableColumn("notify", NotNull=true)]
		private bool notify;
		#endregion
			
		#region Constructors
		/// <summary> 
		/// Create a new object by specifying all fields (except the auto-generated primary key field). 
		/// </summary> 
		public Program(int idChannel, DateTime startTime, DateTime endTime, string title, string description, string genre, bool notify)
		{
			isChanged = true;
			this.idChannel = idChannel;
			this.startTime = startTime;
			this.endTime = endTime;
			this.title = title;
			this.description = description;
			this.genre = genre;
			this.notify = notify;
		}
			
		/// <summary> 
		/// Create an object from an existing row of data. This will be used by Gentle to 
		/// construct objects from retrieved rows. 
		/// </summary> 
		public Program(int idProgram, int idChannel, DateTime startTime, DateTime endTime, string title, string description, string genre, bool notify)
		{
			this.idProgram = idProgram;
			this.idChannel = idChannel;
			this.startTime = startTime;
			this.endTime = endTime;
			this.title = title;
			this.description = description;
			this.genre = genre;
			this.notify = notify;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Indicates whether the entity is changed and requires saving or not.
		/// </summary>
		public bool IsChanged
		{
			get	{ return isChanged; }
		}

		/// <summary>
		/// Property relating to database column idProgram
		/// </summary>
		public int IdProgram
		{
			get { return idProgram; }
		}

		/// <summary>
		/// Property relating to database column idChannel
		/// </summary>
		public int IdChannel
		{
			get { return idChannel; }
			set { isChanged |= idChannel != value; idChannel = value; }
		}

		/// <summary>
		/// Property relating to database column startTime
		/// </summary>
		public DateTime StartTime
		{
			get { return startTime; }
			set { isChanged |= startTime != value; startTime = value; }
		}

		/// <summary>
		/// Property relating to database column endTime
		/// </summary>
		public DateTime EndTime
		{
			get { return endTime; }
			set { isChanged |= endTime != value; endTime = value; }
		}

		/// <summary>
		/// Property relating to database column title
		/// </summary>
		public string Title
		{
			get { return title; }
			set { isChanged |= title != value; title = value; }
		}

		/// <summary>
		/// Property relating to database column description
		/// </summary>
		public string Description
		{
			get { return description; }
			set { isChanged |= description != value; description = value; }
		}

		/// <summary>
		/// Property relating to database column genre
		/// </summary>
		public string Genre
		{
			get { return genre; }
			set { isChanged |= genre != value; genre = value; }
		}

		/// <summary>
		/// Property relating to database column notify
		/// </summary>
		public bool Notify
		{
			get { return notify; }
			set { isChanged |= notify != value; notify = value; }
		}
		#endregion

		#region Storage and Retrieval
	
		/// <summary>
		/// Static method to retrieve all instances that are stored in the database in one call
		/// </summary>
		public static IList ListAll()
		{
			return Broker.RetrieveList(typeof(Program));
		}

		/// <summary>
		/// Retrieves an entity given it's id.
		/// </summary>
		public static Program Retrieve(int id)
		{
			// Return null if id is smaller than seed and/or increment for autokey
			if(id<1) 
			{
				return null;
			}
			Key key = new Key(typeof(Program), true, "idProgram", id);
			return Broker.RetrieveInstance(typeof(Program), key) as Program;
		}
		
		/// <summary>
		/// Retrieves an entity given it's id, using Gentle.Framework.Key class.
		/// This allows retrieval based on multi-column keys.
		/// </summary>
		public static Program Retrieve(Key key)
		{
			return Broker.RetrieveInstance(typeof(Program), key) as Program;
		}
		
		/// <summary>
		/// Persists the entity if it was never persisted or was changed.
		/// </summary>
		public override void Persist()
		{
			if (IsChanged || !IsPersisted)
			{
				base.Persist();
				isChanged = false;
			}
		}

		#endregion


		#region Relations

		/// <summary>
		/// Get a list of Favorite referring to the current entity.
		/// </summary>
		public IList ReferringFavorite()
		{
			//select * from 'foreigntable'
			SqlBuilder sb = new SqlBuilder(StatementType.Select, typeof(Favorite));

			// where foreigntable.foreignkey = ourprimarykey
			sb.AddConstraint(Operator.Equals, "idProgram", idProgram);

			// passing true indicates that we'd like a list of elements, i.e. that no primary key
			// constraints from the type being retrieved should be added to the statement
			SqlStatement stmt = sb.GetStatement(true);

			// execute the statement/query and create a collection of User instances from the result set
			return ObjectFactory.GetCollection(typeof(Favorite), stmt.Execute());

			// TODO In the end, a GentleList should be returned instead of an arraylist
			//return new GentleList( typeof(Favorite), this );
		}
		/// <summary>
		///
		/// </summary>
		public Channel ReferencedChannel()
		{
			return Channel.Retrieve(IdChannel);
		}
		#endregion


    public void Delete()
    {
      IList list = ReferringFavorite();
      foreach (Favorite favorite in list)
        favorite.Remove();
    }
    /// <summary>
    /// Checks if the program is running between the specified start and end time/dates
    /// </summary>
    /// <param name="tStartTime">Start date and time</param>
    /// <param name="tEndTime">End date and time</param>
    /// <returns>true if program is running between tStartTime-tEndTime</returns>
    public bool RunningAt(DateTime tStartTime, DateTime tEndTime)
    {
      DateTime dtStart = StartTime;
      DateTime dtEnd = EndTime;

      bool bRunningAt = false;
      if (dtEnd >= tStartTime && dtEnd <= tEndTime) bRunningAt = true;
      if (dtStart >= tStartTime && dtStart <= tEndTime) bRunningAt = true;
      if (dtStart <= tStartTime && dtEnd >= tEndTime) bRunningAt = true;
      return bRunningAt;
    }

    /// <summary>
    /// Checks if the program is running at the specified date/time
    /// </summary>
    /// <param name="tCurTime">date and time</param>
    /// <returns>true if program is running at tCurTime</returns>
    public bool IsRunningAt(DateTime tCurTime)
    {
      bool bRunningAt = false;
      if (tCurTime >= StartTime && tCurTime <= EndTime) bRunningAt = true;
      return bRunningAt;
    }
	}
}

using Microsoft.EntityFrameworkCore;
using CollaborativeDrawingBoard.Models;

namespace CollaborativeDrawingBoard.Data;

  public class DrawingBoardContext : DbContext
  {
      public DrawingBoardContext(DbContextOptions<DrawingBoardContext> options) : base(options)
      {
      }

      public DbSet<DrawingBoard> DrawingBoards { get; set; }
  }

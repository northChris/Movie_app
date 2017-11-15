using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MovieApp.Models;

namespace MovieApp.Controllers
{
    [RoutePrefix("Api/Movies")]
    public class MoviesController : ApiController
    {
        private MovieAppContext db = new MovieAppContext();

        // GET: api/Movies
        [Route("OwnedMovies")]
        public List<Movie> GetOwnedMovies()
        {
            return db.Movie.Where(x => x.IsOwned == true).ToList();
        }

        [Route("WantedMovies")]
        public List<Movie> GetWantedMovies()
        {
            return db.Movie.Where(x => !x.IsOwned).ToList();
        }
        
        [Route("DeleteMovies")]
        public IHttpActionResult DeleteMovies(int id)
        {
            var deletedMovie = db.Movie.FirstOrDefault(x => x.id == id);

            db.Movie.Remove(deletedMovie);

            db.SaveChanges();

            return Ok();
        }

        [Route("AddMovies")]
        public IHttpActionResult PostMovies([FromBody] Movie movie)
        {
            if(movie.id == 0)
            {
                db.Movie.Add(movie);
            }
            else
            {
                var existingMovie = db.Movie.FirstOrDefault(x => x.id == movie.id);
                existingMovie.Title = movie.Title;
                existingMovie.IsOwned = movie.IsOwned;
                existingMovie.Description = movie.Description;
                existingMovie.Price = movie.Price;
                existingMovie.IsActive = movie.IsActive;
            }
            db.SaveChanges();

            return Ok();
        }
        [Route("GetMovie")]
        public Movie GetMovie(int id)
        {
            return db.Movie.FirstOrDefault(x => x.id == id);
        }
        [Route("UsersTable")]
        public IHttpActionResult GetUsers([FromBody] string username, string password)
        {
            var result = db.User.FirstOrDefault(x => x.Username == username && x.Password == password).id;
            return Ok(result);
        }
        [Route("AddUsers")]
        public IHttpActionResult PostUsers([FromBody] User user)
        {
            var check = db.User.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
            if ( check == null)
            {
                db.User.Add(user);
            };
            db.SaveChanges();
            return Ok();
        }




        //// GET: api/Movies/5
        //[ResponseType(typeof(Movie))]
        //public async Task<IHttpActionResult> GetMovie(int id)
        //{
        //    Movie movie = await db.Movie.FindAsync(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(movie);
        //}

        //// PUT: api/Movies/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutMovie(int id, Movie movie)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != movie.id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(movie).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MovieExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Movies
        //[ResponseType(typeof(Movie))]
        //public async Task<IHttpActionResult> PostMovie(Movie movie)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Movie.Add(movie);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = movie.id }, movie);
        //}

        //// DELETE: api/Movies/5
        //[ResponseType(typeof(Movie))]
        //public async Task<IHttpActionResult> DeleteMovie(int id)
        //{
        //    Movie movie = await db.Movie.FindAsync(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Movie.Remove(movie);
        //    await db.SaveChangesAsync();

        //    return Ok(movie);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool MovieExists(int id)
        //{
        //    return db.Movie.Count(e => e.id == id) > 0;
        //}
    }
}
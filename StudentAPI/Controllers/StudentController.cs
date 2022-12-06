﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.DALs;
using StudentAPI.Models;

namespace MaxCodeFirst.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly PracticeDbContext _dbContext;

        public StudentController(PracticeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var students = await _dbContext.Students.ToListAsync();
            return Ok(students);
        } 

        [HttpGet]
        [Route("get-student-by-id")]
        public async Task<IActionResult> GetStudentByIdAsync(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Students student)
        {
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync();
            return Created($"/get-student-by-id?id={student.Id}", student);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Students studentToUpdate)
        {
            _dbContext.Students.Update(studentToUpdate);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var studentToDelete = await _dbContext.Students.FindAsync(id);
            if (studentToDelete == null)
            {
                return NotFound();
            }
            _dbContext.Students.Remove(studentToDelete);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
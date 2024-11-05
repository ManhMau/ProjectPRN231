using AutoMapper;
using BussinessObject.DTOS;
using BussinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMemberController : ControllerBase
    {
        private readonly IGroupMemberRepository _groupMemberRepository;
        private readonly IMapper _mapper;

        public GroupMemberController(IGroupMemberRepository groupMemberRepository, IMapper mapper)
        {
            _groupMemberRepository = groupMemberRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupMemberDTO>>> GetGroupMembers()
        {
            var groupMembers = await _groupMemberRepository.GetAllGroupMembersAsync();
            return Ok(groupMembers);  // Trả về JSON thay vì View
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupMemberDTO>> GetGroupMember(int id)
        {
            var groupMember = await _groupMemberRepository.GetGroupMemberByIdAsync(id);
            if (groupMember == null)
            {
                return NotFound();
            }

            return Ok(groupMember);  // Trả về JSON thay vì View
        }

        [HttpPost]
        public async Task<ActionResult> PostGroupMember(GroupMemberDTO groupMemberDTO)
        {
            await _groupMemberRepository.AddGroupMemberAsync(groupMemberDTO);
            return CreatedAtAction(nameof(GetGroupMember), new { id = groupMemberDTO.Id }, groupMemberDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupMember(int id, GroupMemberDTO groupMemberDTO)
        {
            if (id != groupMemberDTO.Id)
            {
                return BadRequest();
            }

            await _groupMemberRepository.UpdateGroupMemberAsync(groupMemberDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupMember(int id)
        {
            await _groupMemberRepository.DeleteGroupMemberAsync(id);
            return NoContent();
        }

        // Tìm kiếm GroupMembers theo NameGroup
        [HttpGet("SearchByNameGroup")]
        public async Task<IActionResult> SearchByNameGroup(string nameGroup)
        {
            try
            {
                var groupMembers = await _groupMemberRepository.SearchByNameGroup(nameGroup);

                // Trả về JSON kết quả đã được ánh xạ sang DTO
                return Ok(_mapper.Map<List<GroupMemberDTO>>(groupMembers));
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và trả về lỗi
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Sắp xếp GroupMembers
        [HttpGet("SortGroupMembers")]
        public async Task<IActionResult> SortGroupMembers(string sortBy = "id", string sortDirection = "asc")
        {
            try
            {
                var groupMembers = await _groupMemberRepository.SortBy(sortBy, sortDirection);

                // Trả về JSON kết quả đã được ánh xạ sang DTO
                return Ok(_mapper.Map<List<GroupMemberDTO>>(groupMembers));
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và trả về lỗi
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}

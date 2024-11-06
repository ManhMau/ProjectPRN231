using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class GroupMemberController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(string searchNameGroup = "", string sortOption = "id", string sortDirection = "asc")
        {
            List<GroupMemberDTO> groupMembers;

            // Thực hiện tìm kiếm nếu có tên nhóm
            if (!string.IsNullOrEmpty(searchNameGroup))
            {
                groupMembers = await APIFunction.SearchGroupMembersByName(searchNameGroup);
            }
            else
            {
                groupMembers = await APIFunction.GetListGroupMembers();
            }

            // Sắp xếp groupMembers theo sortOption và sortDirection
            groupMembers = SortGroupMembers(groupMembers, sortOption, sortDirection);

            // Lưu thông tin tìm kiếm và sắp xếp trong ViewData để hiển thị trên giao diện
            ViewData["Message"] = TempData["Message"];
            ViewData["SearchNameGroup"] = searchNameGroup;
            ViewData["SortOption"] = sortOption;
            ViewData["SortDirection"] = sortDirection;

            return View(groupMembers);
        }

        // Phương thức sắp xếp
        private List<GroupMemberDTO> SortGroupMembers(List<GroupMemberDTO> groupMembers, string sortOption, string sortDirection)
        {
            switch (sortOption.ToLower())
            {
                case "id":
                    groupMembers = sortDirection.ToLower() == "desc"
                        ? groupMembers.OrderByDescending(gm => gm.Id).ToList()
                        : groupMembers.OrderBy(gm => gm.Id).ToList();
                    break;

                case "namegroup":
                    groupMembers = sortDirection.ToLower() == "desc"
                        ? groupMembers.OrderByDescending(gm => gm.NameGroup).ToList()
                        : groupMembers.OrderBy(gm => gm.NameGroup).ToList();
                    break;
            }

            return groupMembers;
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new GroupMemberDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupMemberDTO model)
        {
            int result = await APIFunction.CreateGroupMemberAsync(model);

            if (result == 200 || result == 201)
            {
                TempData["Message"] = "GroupMember created successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Error creating GroupMember. Please try again.";
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var groupMember = await APIFunction.GetGroupMemberById(id);

            if (groupMember == null)
            {
                TempData["Message"] = "GroupMember not found.";
                return RedirectToAction("Index");
            }

            return View(groupMember);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GroupMemberDTO model)
        {
            int result = await APIFunction.UpdateGroupMember(model);

            if (result == 200 || result == 204) // 200 OK or 204 No Content are both success
            {
                TempData["SuccessMessage"] = "Group Member updated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error updating Group Member. Please try again.";
            }

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await APIFunction.DeleteGroupMember(id);
            if (result == 200)
            {
                TempData["Message"] = "GroupMember deleted successfully!";
            }
            else
            {
                TempData["Message"] = "Error deleting GroupMember. Please try again.";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var groupMember = await APIFunction.GetGroupMemberById(id);

            if (groupMember == null)
            {
                return NotFound();
            }

            return View(groupMember);
        }

        [HttpGet]
        public async Task<IActionResult> ListUserGroup(int id)
        {
            var groupMember = await APIFunction.GetGroupMemberById(id);

            if (groupMember == null || groupMember.Users == null || !groupMember.Users.Any())
            {
                return NotFound("No users found in this group.");
            }

            // Chỉ truyền danh sách Users vào view
            return View(groupMember.Users);
        }
    }
}

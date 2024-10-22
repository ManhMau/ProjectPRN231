using BussinessObject.DTOS;
using BussinessObject.DTOS.Common;
using BussinessObject.DTOS.User;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using BusinessObject.DTO;


namespace WebClient
{
    public class APIFunction
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<APIResponseModel> LoginAsync(LoginVM model)
        {
            string url = "http://localhost:5121/api/Users/Login";

          
            HttpResponseMessage response = await client.PostAsJsonAsync(url, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<APIResponseModel>();
            }

            return new APIResponseModel { IsSuccess = false }; 
        }

        public static async Task<APIResponseModel> RegisterAsync(RegisterVM model)
        {
            string url = "http://localhost:5121/api/Users/Register";

        
            HttpResponseMessage response = await client.PostAsJsonAsync(url, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<APIResponseModel>();
            }

            return new APIResponseModel { IsSuccess = false }; 
        }
        public static async Task<List<DocumentDTO>> GetListDocuments()
        {
            string url = "http://localhost:5121/api/Documents/GetAllDocuments";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<DocumentDTO>>();
            }
            return new List<DocumentDTO>();

        }
        public static async Task<DocumentDTO> GetDocumentById(int id)
        {
            string url = $"http://localhost:5121/api/Documents/GetDocumentById/{id}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<DocumentDTO>();
            }
            return new DocumentDTO();

        }
        public static async Task<int> CreateDocumentAsync(DocumentDTO p)
        {
            string url = "http://localhost:5121/api/Documents/AddDocument";
            var response = await client.PostAsJsonAsync(url, p);
            if (response.IsSuccessStatusCode)
            {
                return 200;
            }
            return -1;
        }
        public static async Task<int> UpdateDocument(DocumentDTO p)
        {
            string url = "http://localhost:5121/api/Documents/UpdateDocument";
            var response = await client.PutAsJsonAsync(url, p);

            if (response.IsSuccessStatusCode)
            {
                return 200;
            }

            return -1;
        }
        public static async Task<int> DeleteDocument(int id)
        {
            string url = $"http://localhost:5121/api/Documents/Delete/{id}";
            var response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return 200;
            }
            return -1;
        }

        public static async Task<List<DocTypeMapper>> GetListDocType()
        {
            string url = "http://localhost:5121/api/DocType/GetAllCategories";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<DocTypeMapper>>();
            }
            return new List<DocTypeMapper>();

        }
        public static async Task<List<DocumentDTO>> SearchDocumentsByTitle(string title)
        {
            string url = $"http://localhost:5121/api/Documents/SearchByTitle?title={Uri.EscapeDataString(title)}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<DocumentDTO>>();
            }
            return new List<DocumentDTO>();
        }

    
        public static async Task<List<DocumentDTO>> SortDocumentsByDate(bool descending)
        {
            string url = $"http://localhost:5121/api/Documents/SortByDate?descending={descending}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<DocumentDTO>>();
            }
            return new List<DocumentDTO>();
        }
        public static async Task<List<UserDTO>> GetAllUsersAsync()
        {
            string url = "http://localhost:5121/api/Users/GetAllUser";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<UserDTO>>();
            }

            return new List<UserDTO>();
        }
        public static async Task<UserDTO> GetUserByIdAsync(string id)
        {
            string url = $"http://localhost:5121/api/Users/GetUserById/{id}";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDTO>();
            }

            return new UserDTO();
        }
        public static async Task<int> UpdateUserAsync(UserDTO user)
        {
            string url = "http://localhost:5121/api/Users/UpdateUser";
            HttpResponseMessage response = await client.PutAsJsonAsync(url, user);

            if (response.IsSuccessStatusCode)
            {
                return 200;  // Success status code
            }

            return -1;  // Failure status code
        }
        public static async Task<int> DeleteUserAsync(string id)
        {
            string url = $"http://localhost:5121/api/Users/Delete/{id}";
            HttpResponseMessage response = await client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return 200;  // Success status code
            }

            return -1;  // Failure status code
        }
        public static async Task<UserDTO> GetUserByName(string name)
        {
            string url = $"http://localhost:5121/api/Users/GetUserByName/{name}";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDTO>();
            }

            return new UserDTO();
        }
        public static async Task<List<UserDTO>> SearchUser(string title)
        {
            string url = $"http://localhost:5121/api/Users/SearchUser?title={Uri.EscapeDataString(title)}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<UserDTO>>();
            }
            return new List<UserDTO>();
        }


        public static async Task<List<UserDTO>> SortUserByName(bool descending)
        {
            string url = $"http://localhost:5121/api/Users/SortByName?descending={descending}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<UserDTO>>();
            }
            return new List<UserDTO>();
        }


        // 1. Lấy danh sách tất cả GroupMembers
        public static async Task<List<GroupMemberDTO>> GetListGroupMembers()
        {
            string url = "http://localhost:5121/api/GroupMember";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<GroupMemberDTO>>();
            }
            return new List<GroupMemberDTO>();
        }

        // 2. Lấy GroupMember theo Id
        public static async Task<GroupMemberDTO> GetGroupMemberById(int id)
        {
            string url = $"http://localhost:5121/api/GroupMember/{id}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GroupMemberDTO>();
            }
            return new GroupMemberDTO();
        }

        // 3. Tạo mới GroupMember
        public static async Task<int> CreateGroupMemberAsync(GroupMemberDTO model)
        {
            string url = "http://localhost:5121/api/GroupMember";
            HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
            return (int)response.StatusCode;
        }

        // 4. Cập nhật GroupMember
        public static async Task<int> UpdateGroupMember(GroupMemberDTO model)
        {
            string url = $"http://localhost:5121/api/GroupMember/{model.Id}";
            HttpResponseMessage response = await client.PutAsJsonAsync(url, model);
            return (int)response.StatusCode;
        }

        // 5. Xóa GroupMember
        public static async Task<int> DeleteGroupMember(int id)
        {
            string url = $"http://localhost:5121/api/GroupMember/{id}";
            HttpResponseMessage response = await client.DeleteAsync(url);
            return (int)response.StatusCode;
        }

        // 6. Tìm kiếm GroupMember theo tên nhóm (NameGroup)
        public static async Task<List<GroupMemberDTO>> SearchGroupMembersByName(string nameGroup)
        {
            string url = $"http://localhost:5121/api/GroupMember/SearchByNameGroup?nameGroup={nameGroup}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<GroupMemberDTO>>();
            }
            return new List<GroupMemberDTO>();
        }

        // 7. Sắp xếp GroupMembers theo thuộc tính được chỉ định
        public static async Task<List<GroupMemberDTO>> SortGroupMembers(string sortBy, string sortDirection)
        {
            string url = $"http://localhost:5121/api/GroupMember/SortGroupMembers?sortBy={sortBy}&sortDirection={sortDirection}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<GroupMemberDTO>>();
            }
            return new List<GroupMemberDTO>();
        }


    }

}

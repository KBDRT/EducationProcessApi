import axios from "axios"

export const deleteTeacher = async (data) => 
{
    if (data == null)
    {
        return false;
    }

    try {
        const response = await axios.delete("https://localhost:7032/Teachers/single/", 
            {data: { teacherId: data.id },
            headers: {
                'Content-Type': 'application/json'
            }
        })
        return response.data
    } catch (error) {
        console.log(error)
        return false;
    }
}

import axios from "axios"

export const getTeachers = async () => 
{
    try {
        const response = await axios.get("https://localhost:7032/Teachers?size=10")
        return response.data
    } catch (error) {
        console.log(error)
    }
}


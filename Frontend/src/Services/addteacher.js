import axios from "axios"

export const addNewTeacher = async (data) => 
{
    if (data == null)
    {
        return false;
    }

    let teacher = data.teacher;

    try {
        const response = await axios.post("https://localhost:7032/Teachers/", 
            {
                surname : teacher.surname,
                name : teacher.name,
                patronymic : teacher.patronymic,
                birthDate : teacher.birthDate,
            }
        )
        return response.data
    } catch (error) {
        console.log(error)
        return false;
    }
}
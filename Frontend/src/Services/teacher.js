import axios from "axios"

export const changeTeacher = async (data) => 
{
    if (data == null)
    {
        return;
    }

    let teacher = data.teacher;

    try {
        const response = await axios.put("https://localhost:7032/Teachers/", 
            {
                id : teacher.id,
                surname : teacher.surname,
                name : teacher.name,
                patronymic : teacher.patronymic,
                birthDate : teacher.birthDate,
            }
        )
        return response.data
    } catch (error) {
        console.log(error)
    }
}
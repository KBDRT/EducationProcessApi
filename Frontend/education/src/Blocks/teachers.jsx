import { getTeachers } from '@/Services/teachers';
import '../App.css'
import { Table, Box} from '@chakra-ui/react'
import { useEffect, useState } from 'react';
import PrintTeacherRow from './teacher';

export default function Teachers() {
    const [teachers, setTeachers] = useState([]);

    useEffect(() => {
    
    const fetchTeachers = async () => {
        let data = await getTeachers();
        setTeachers(data);
    }

    fetchTeachers();
    
  }, [])


  const listItems = teachers.map(teacher =>
    <li>{teacher.id}</li>
  );

  return (
    <Box w="50%" mx="auto">
      <Table.Root size="sm" className="text-[16px]">
        <Table.Header>
          <Table.Row>
            <Table.ColumnHeader style={{ width: "25%", textAlign: "center" }}>
              Фамилия
            </Table.ColumnHeader>
            <Table.ColumnHeader style={{ width: "25%", textAlign: "center" }} >
              Имя
            </Table.ColumnHeader>
            <Table.ColumnHeader style={{ width: "25%", textAlign: "center" }}>
              Отчество
            </Table.ColumnHeader>
            <Table.ColumnHeader style={{ width: "25%", textAlign: "center" }}>
              Дата рождения
            </Table.ColumnHeader>
            <Table.ColumnHeader style={{ width: "25%", textAlign: "center" }}>
              Изменение
            </Table.ColumnHeader>
          </Table.Row>
        </Table.Header>

        <Table.Body>
            {teachers.map((teacher) => {
                return <PrintTeacherRow key={teacher.id} teacher={teacher} />
            })}
        </Table.Body>
      </Table.Root>
    </Box>
  )
}


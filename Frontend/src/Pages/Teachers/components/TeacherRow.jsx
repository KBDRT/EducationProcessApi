import '../../../App.css'
import { Table } from '@chakra-ui/react'
import { format, parseISO, differenceInYears } from 'date-fns'
import { useState } from 'react';
import EditTeacher from './EditTeacher'

export default function PrintTeacherRow({teacher}) {
 if (!teacher) {
    return null;
  }

  const [oldData, setOldData] = useState(teacher);
  const [age, setAge] = useState(differenceInYears(new Date(), parseISO(teacher.birthDate)));
  const [agePost, setAgePost] = useState(getAgeWord(age));

  const handleTeacherUpdate = (updatedTeacher) => {
    setOldData(updatedTeacher);
    let age = differenceInYears(new Date(), parseISO(updatedTeacher.birthDate));
    setAge(age);
    setAgePost(getAgeWord(age));
  };

  return (
    <Table.Row 
      key={oldData.id}
      className="hover:bg-blue-50 transition-colors duration-200"
      _hover={{ bg: "blue.50" }}
    >

      <Table.Cell 
        className="text-center font-medium text-gray-800"
        borderBottomWidth="1px"
        borderColor="blue.100"
      >
        <span className="text-blue-900">{oldData.surname}</span>
      </Table.Cell>
        
      <Table.Cell 
        className="text-center text-gray-700"
        borderBottomWidth="1px"
        borderColor="blue.100"
      >
        {oldData.name}
      </Table.Cell>
      
      <Table.Cell 
        className="text-center text-gray-700"
        borderBottomWidth="1px"
        borderColor="blue.100"
      >
        {oldData.patronymic}
      </Table.Cell>
      
      <Table.Cell 
        className="text-center"
        borderBottomWidth="1px"
        borderColor="blue.100"
      >
        <div className="flex flex-col items-center">
          <span className="text-gray-700">
            {format(parseISO(oldData.birthDate), 'dd.MM.yyyy') } ({age} {agePost})
          </span>
          {/* <span className="text-sm text-blue-600"></span> */}
        </div>
      </Table.Cell> 
      
      <Table.Cell 
        className="text-center"
        borderBottomWidth="1px"
        borderColor="blue.100">
        <EditTeacher teacher={oldData} onUpdate={handleTeacherUpdate}  />
      </Table.Cell> 

    </Table.Row>
  )
}

function getAgeWord(age) {
  const lastDigit = age % 10;
  const lastTwoDigits = age % 100;
  
  if (lastTwoDigits >= 11 && lastTwoDigits <= 14) {
    return 'лет';
  }
  if (lastDigit === 1) {
    return 'год';
  }
  if (lastDigit >= 2 && lastDigit <= 4) {
    return 'года';
  }
  return 'лет';
}

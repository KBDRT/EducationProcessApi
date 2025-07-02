import '../App.css'
import { Table } from '@chakra-ui/react'
import { format, parseISO, differenceInYears } from 'date-fns'
import { IconButton } from "@chakra-ui/react"
import { MdEdit  } from "react-icons/md"
import { Button, CloseButton, Dialog, Portal, Field, Fieldset, Input, } from "@chakra-ui/react"
import { changeTeacher } from '@/Services/teacher';
import { useState } from 'react';

export default function PrintTeacherRow({teacher}) {
 if (!teacher) {
    return null;
  }

  let age = differenceInYears(new Date(), parseISO(teacher.birthDate));
  let age_post = getAgeWord(age);

  const [formData, setFormData] = useState(teacher);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    changeTeacher({ teacher: formData });
  };

  return (
    <Table.Row key={teacher.id}>
        <Table.Cell style={{ textAlign: "center" }}> {teacher.surname} </Table.Cell>
        <Table.Cell style={{ textAlign: "center" }}> {teacher.name} </Table.Cell>
        <Table.Cell style={{ textAlign: "center" }}> {teacher.patronymic} </Table.Cell>
        <Table.Cell style={{ textAlign: "center" }}> {format(parseISO(teacher.birthDate), 'dd.MM.yyyy')} ({age} {age_post}) </Table.Cell> 
        <Table.Cell style={{ textAlign: "center" }}> 
            
            <Dialog.Root>
                <Dialog.Trigger asChild>
                <IconButton size="xs" bg="blue.500">
                    <MdEdit  />
                </IconButton>
                </Dialog.Trigger>
                <Portal>
                    <Dialog.Backdrop />
                    <Dialog.Positioner>
                    <Dialog.Content>
                        <Dialog.Header>
                        <Dialog.Title className="text-[20px]">Редактирование педагога</Dialog.Title>
                        </Dialog.Header>
                        <Dialog.Body>
                       
                        <Fieldset.Root >
                            <Fieldset.Content>
                                <Field.Root orientation="horizontal">
                                    <Field.Label flex="1" className="text-[16px]">Фамилия</Field.Label>
                                    <Input name="surname" className="text-[16px]" flex="2" value={formData.surname} onChange={handleChange}/>
                                </Field.Root>

                                <Field.Root orientation="horizontal">
                                    <Field.Label flex="1" className="text-[16px]">Имя</Field.Label>
                                    <Input name="name" className="text-[16px]" flex="2" value={formData.name} onChange={handleChange}/>
                                </Field.Root>

                                <Field.Root orientation="horizontal">
                                    <Field.Label flex="1" className="text-[16px]">Отчество</Field.Label>
                                    <Input name="patronomic" flex="2" className="text-[16px]" value={formData.patronymic} onChange={handleChange}/>
                                </Field.Root>

                                <Field.Root orientation="horizontal">
                                    <Field.Label flex="1" className="text-[16px]">Дата рождения</Field.Label>
                                    <Input name="birthDate" type="date" flex="2" className="text-[16px]" value={formData.birthDate} onChange={handleChange}/>
                                </Field.Root>
                                
                            </Fieldset.Content>

                        </Fieldset.Root>

                        </Dialog.Body>
                        <Dialog.Footer>
                        <Button bg="green" type="submit" onClick={handleSubmit}>Сохранить</Button>
                        <Dialog.ActionTrigger asChild>
                            <Button bg="red.600">Отмена</Button>
                        </Dialog.ActionTrigger>
                        </Dialog.Footer>
                        <Dialog.CloseTrigger asChild>
                        <CloseButton size="sm" />
                        </Dialog.CloseTrigger>
                    </Dialog.Content>
                    </Dialog.Positioner>
                </Portal>
                </Dialog.Root>

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

import '../../../App.css'
import { IconButton } from "@chakra-ui/react"
import { MdEdit  } from "react-icons/md"
import { Button, CloseButton, Dialog, Portal, Field, Fieldset, Input, } from "@chakra-ui/react"
import { changeTeacher } from '@/Services/teacher';
import { useState } from 'react';
import { showSuccessToast } from "../../../Utils/alerts";
import { deleteTeacher } from '@/Services/deleteacher';
import { useTeachersContext } from '../../../Providers/TeachersProvider';

export default function EditTeacher({ teacher, onUpdate }) {

  const { removeTeacherFromState } = useTeachersContext();

  const [formData, setFormData] = useState(teacher);
  const [oldData, setOldData] = useState(teacher);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    let result = await changeTeacher({ teacher: formData });

    if (result != false)
    {
      showSuccessToast("Сохранено!");
      onUpdate(formData);
      setOldData(formData);
    }
  };

  const handleDelete = async (e) => {
    e.preventDefault();
    let result = await deleteTeacher({ id: formData.id });

    if (result != false)
    {
      showSuccessToast("Удалено!");
      removeTeacherFromState(formData.id);
    }
  };
  
  const cancelModal = () => {
    setFormData(oldData);
  };

 return (
    <Dialog.Root onExitComplete={cancelModal}>

      <Dialog.Trigger asChild>
        <IconButton 
          size="sm"  
          colorScheme="blue"
          aria-label="Редактировать"
          variant="outline"
          borderRadius="full"
          className="hover:bg-blue-100 border-blue-300 text-blue-600"
          _hover={{ bg: "blue.100" }}
        >
        <MdEdit size={18} />
        </IconButton>
      </Dialog.Trigger>
      
      <Portal>
        <Dialog.Backdrop bg="blackAlpha.600" backdropFilter="blur(2px)" />
        <Dialog.Positioner>
          <Dialog.Content 
            bg="white"
            borderRadius="xl"
            boxShadow="2xl"
            className="p-8"
          >

            <Dialog.Header className="pb-6">
              <Dialog.Title className="text-2xl font-bold text-blue-800">
                Редактирование педагога
              </Dialog.Title>
            </Dialog.Header>
            
            <Dialog.Body className="py-1 space-y-1">
              <Fieldset.Root>
                <Fieldset.Content spaceY={1}>
                  <Field.Root orientation="horizontal" alignItems="center">
                    <Field.Label 
                      flex="1" 
                      className="text-base font-medium text-blue-900"
                    >
                      Фамилия
                    </Field.Label>
                    <Input 
                      name="surname" 
                      flex="2" 
                      size="md"
                      borderRadius="lg"
                      borderColor="blue.200"
                      _hover={{ borderColor: "blue.300" }}
                      _focus={{ 
                        borderColor: "blue.500", 
                        boxShadow: "0 0 0 2px rgba(66, 153, 225, 0.3)" 
                      }}
                      value={formData.surname} 
                      onChange={handleChange}
                    />
                  </Field.Root>

                  <Field.Root orientation="horizontal" alignItems="center">
                    <Field.Label 
                      flex="1" 
                      className="text-base font-medium text-blue-900"
                    >
                      Имя
                    </Field.Label>
                    <Input 
                      name="name" 
                      flex="2" 
                      size="md"
                      borderRadius="lg"
                      borderColor="blue.200"
                      _hover={{ borderColor: "blue.300" }}
                      value={formData.name} 
                      onChange={handleChange}
                    />
                  </Field.Root>

                  <Field.Root orientation="horizontal" alignItems="center">
                    <Field.Label 
                      flex="1" 
                      className="text-base font-medium text-blue-900"
                    >
                      Отчество
                    </Field.Label>
                    <Input 
                      name="patronymic" 
                      flex="2" 
                      size="md"
                      borderRadius="lg"
                      borderColor="blue.200"
                      _hover={{ borderColor: "blue.300" }}
                      value={formData.patronymic} 
                      onChange={handleChange}
                    />
                  </Field.Root>

                  <Field.Root orientation="horizontal" alignItems="center">
                    <Field.Label 
                      flex="1" 
                      className="text-base font-medium text-blue-900"
                    >
                      Дата рождения
                    </Field.Label>
                    <Input 
                      name="birthDate" 
                      type="date" 
                      flex="2" 
                      size="md"
                      borderRadius="lg"
                      borderColor="blue.200"
                      _hover={{ borderColor: "blue.300" }}
                      value={formData.birthDate} 
                      onChange={handleChange}
                    />
                  </Field.Root>
                </Fieldset.Content>
              </Fieldset.Root>
            </Dialog.Body>
            
            <Dialog.Footer className="flex justify-end space-x-4 pt-6">
              <Button 
                colorScheme="green" 
                size="md"
                bg = "blue"
                onClick={handleSubmit}
                className="px-6 py-2 bg-blue-600 hover:bg-blue-700 text-white"
                _hover={{ bg: "blue.700" }}
              >
                Сохранить изменения
              </Button>
              
              <Dialog.ActionTrigger asChild>
                <Button 
                  colorScheme="red"
                  bg = "red.500"
                  size="md"
                  onClick={handleDelete}
                  className="px-6 py-2 border-red-300 hover:bg-red-50 text-red-700"
                >
                  Удалить
                </Button>
              </Dialog.ActionTrigger>

              <Dialog.ActionTrigger asChild>
                <Button 
                  variant="outline"
                  colorScheme="gray"
                  size="md"
                  // onClick={cancelModal}
                  className="px-6 py-2 border-gray-300 hover:bg-gray-50 text-gray-700"
                >
                  Отменить
                </Button>
              </Dialog.ActionTrigger>
            </Dialog.Footer>
            
      
            <Dialog.CloseTrigger asChild>
              <CloseButton 
                size="md" 
                position="absolute"
                top="6"
                right="6"
                className="text-gray-400 hover:text-gray-600"
                // onClick={cancelModal}
              />
            </Dialog.CloseTrigger>
          </Dialog.Content>
        </Dialog.Positioner>
      </Portal>
    </Dialog.Root>

 )
}
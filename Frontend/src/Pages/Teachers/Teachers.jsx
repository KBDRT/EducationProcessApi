import '../../App.css'
import { Table, Box, Button} from '@chakra-ui/react'
import PrintTeacherRow from './components/TeacherRow';
import { useTeachersContext } from '../../Providers/TeachersProvider';
import HeaderTeachers from './components/Header';
import axios from 'axios';

export default function Teachers() {
  const { teachers } = useTeachersContext();

  const download = async () => {
  try {
    const response = await axios.get("https://localhost:7032/Admin", {
      responseType: 'blob' 
    });

    const url = window.URL.createObjectURL(new Blob([response.data]));
    
    const link = document.createElement('a');
    link.href = url;
    link.setAttribute('download', 'query.txt'); 
    document.body.appendChild(link);
    link.click();
    
    window.URL.revokeObjectURL(url);
    link.remove();

    } catch (error) {
      console.log(error);
      return false;
    }
  }
      
  return (
    <div>

      <HeaderTeachers />

      <Button type="submit" alignSelf="flex-start" bg="blue" onClick={download}>
          Скачать тестовый файл
        </Button>

      
      <Box 
        width={{ base: "100%", md: "90%", lg: "80%", xl: "70%" }}
        mx="auto"
        p={3}
        marginTop = "10px"
        bg="white"
        borderRadius="xl"
        boxShadow="lg"
      >

      <Table.Root 
        variant="striped" 
        colorScheme="blue"
        size="md"
        className="text-[16px] font-sans"
      >
        <Table.Header bg="blue.600">
          <Table.Row>
            <Table.ColumnHeader 
              width="20%"
              py={2}
              textAlign="center"
              color="white"
              fontWeight="semibold"
              fontSize="md"
            >
              Фамилия
            </Table.ColumnHeader>
            
            <Table.ColumnHeader 
              width="20%"
              py={2}
              textAlign="center"
              color="white"
              fontWeight="semibold"
              fontSize="md"
            >
              Имя
            </Table.ColumnHeader>
            
            <Table.ColumnHeader 
              width="20%"
              py={2}
              textAlign="center"
              color="white"
              fontWeight="semibold"
              fontSize="md"
            >
              Отчество
            </Table.ColumnHeader>
            
            <Table.ColumnHeader 
              width="25%"
              py={2}
              textAlign="center"
              color="white"
              fontWeight="semibold"
              fontSize="md"
            >
              Дата рождения
            </Table.ColumnHeader>
            
            <Table.ColumnHeader 
              width="15%"
              py={2}
              textAlign="center"
              color="white"
              fontWeight="semibold"
              fontSize="md"
            >
              Действия
            </Table.ColumnHeader>
          </Table.Row>
        </Table.Header>

        <Table.Body>
          {teachers?.length > 0 ? (
            teachers.map((teacher) => (
              <PrintTeacherRow 
                key={teacher.id} 
                teacher={teacher} 
              />
            ))
          ) : (
            <Table.Row>
              <Table.Cell 
                colSpan={5} 
                textAlign="center"
                py={8}
                color="gray.500"
              >
                {/* <Flex justify="center" align="center">
                  <Icon as={MdInfoOutline} mr={2} />
                  Нет данных для отображения
                </Flex> */}
              </Table.Cell>
            </Table.Row>
          )}
        </Table.Body>
      </Table.Root>
    </Box>
  </div>
  )
}



import '../../App.css'
import { Table, Box} from '@chakra-ui/react'
import PrintTeacherRow from './components/TeacherRow';
import { useTeachersContext } from '../../Providers/TeachersProvider';
import HeaderTeachers from './components/Header';


export default function Teachers() {
  const { teachers } = useTeachersContext();

  return (
    <div>

      <HeaderTeachers />

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



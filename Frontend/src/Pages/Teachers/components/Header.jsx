import '../../../App.css'
import { Button, ButtonGroup, Box, HStack, Text } from "@chakra-ui/react"
import { MdAdd, MdChevronLeft, MdChevronRight } from "react-icons/md";
import AddTeacher from './AddTeacher';
import { useState } from 'react';
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';



export default function HeaderTeachers() {

 const [isOpenAddDialog, SetOpenAddDialog] = useState(false);
 
    const handleTeacherUpdate = () => {
        SetOpenAddDialog(false);
    };


 return (
    <Box 
    width="full"
    bg="white"
    boxShadow="sm"
    position="sticky"
    top={0}
    zIndex="sticky"
    borderBottomWidth="1px"
    borderColor="gray.200">
        
        <Box 
            width={{ base: "100%", md: "90%", lg: "80%", xl: "70%" }}
            mx="auto"
            py={3}
            px={{ base: 4, md: 6 }}
            display="flex"
            justifyContent="space-between"
            alignItems="center">

            <Text fontSize="3xl" fontWeight="bold" color="blue.600">
                Организация
            </Text>

            <Link to="/Direction">
                <Button colorScheme="blue" variant="ghost" size="sm">
                    Направления
                </Button>
            </Link>
          
            <HStack spacing={4}>
            {/* <Button 
                colorScheme="blue" 
                variant="outline"
                size="sm"
            >
                <MdChevronLeft /> Назад
            </Button>
            <Button 
                colorScheme="blue" 
                variant="outline"
                size="sm"
            >
                Вперед <MdChevronRight />
            </Button> */}
            <Button 
                colorScheme="blue"
                variant="ghost"
                size="sm"
                onClick={() => SetOpenAddDialog(true)}
            >
                <MdAdd /> Добавить педагога
            </Button>
            
            {
                isOpenAddDialog && <AddTeacher OnAdded={handleTeacherUpdate}/>
            }
            
            </HStack>
        </Box>
    </Box>

 )
}
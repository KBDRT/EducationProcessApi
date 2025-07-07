import '../../App.css'
import { useState } from 'react';
import {
  Button,
  Field,
  Fieldset,
  For,
  Input,
  NativeSelect,
  Stack,
  Text,
} from "@chakra-ui/react"

import axios from "axios"
axios.defaults.withCredentials = true;



export default function LoginPage() {

  const request = {
    Login: "",
    Password: "",
  };

  const [formData, setFormData] = useState(request);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };


  const loginUser = async () => {
    try {
        const response = await axios.post("https://localhost:7032/login", 
            {
                Login : formData.Login,
                Password : formData.Password,
            }
            ,
            {
                withCredentials: true, 
            }
        )
        return response.data
    } catch (error) {
        console.log(error)
        return false;
    }

  }


  return (
    <div >
       <Fieldset.Root size="sm" maxW="md" mx="auto" pt="10%">
            <Stack>
                <Fieldset.Legend>
                    <Text textStyle="2xl">Авторизация</Text>
                </Fieldset.Legend>
            </Stack>

            <Fieldset.Content mt="20px">
                <Field.Root>
                    <Field.Label>Логин</Field.Label>
                    <Input name="Login" value={formData.Login} onChange={handleChange} />
                </Field.Root>

                <Field.Root>
                    <Field.Label>Пароль</Field.Label>
                    <Input name="Password" type="password" value={formData.Password} onChange={handleChange}/>
                </Field.Root>

            </Fieldset.Content>

            <Button type="submit" alignSelf="flex-start" bg="blue" onClick={loginUser}>
                Войти
            </Button>
        </Fieldset.Root>
    </div>
  );
}
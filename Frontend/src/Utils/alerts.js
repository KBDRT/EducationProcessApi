import { Toaster, toaster } from "@/components/ui/toaster"

export const showSuccessToast = (message) => {
     toaster.create({
      description: message,
      type: "success",
      duration: 2500,
    });
};
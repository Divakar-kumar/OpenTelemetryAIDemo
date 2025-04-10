using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;

namespace OpenTelemetryAIDemo.Services
{
   public interface IKernelService
   {
      Task<ChatMessageContent> GetChatMessageContentAsync(Kernel kernel, string prompt, OpenAIPromptExecutionSettings? promptExecutionSettings=null);

   }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Corrupted
{
    [RequireComponent(typeof(RigidbodyMovement))]
    public class PlayerMovementCommandListener : CorruptedCommandListener<RigidbodyMovement>
    {
        public override RigidbodyMovement GetReceiver()
        {
            return GetComponent<RigidbodyMovement>();
        }


        private void FixedUpdate()
        {
            foreach(CommandListener cl in buttons)
            {
                if (cl.command is IFixedUpdateListener<RigidbodyMovement>)
                    (cl.command as IFixedUpdateListener<RigidbodyMovement>).OnFixedUpdate(receiver);
            }
            foreach(CommandAxisListener cl in axes)
            {
                if (cl.command is IFixedUpdateListener<RigidbodyMovement>)
                    (cl.command as IFixedUpdateListener<RigidbodyMovement>).OnFixedUpdate(receiver);
            }
        }
    }

    
}
